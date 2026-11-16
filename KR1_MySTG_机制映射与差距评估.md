# KR1 × MySTG 机制映射与差距评估

> 输入 A：`D:\projects\KingdomRush\1代\资产分析\`（70 敌人 / 20 塔 / 47 技能 / 9 伤害类型）
> 输入 B：`D:\workplace\MySTG`（MyFramework_TDDemo，Rogue-like 塔防）
> 目的：判断 KR1 的机制能有多少直接长在 MySTG 上，以及长不上去的卡在哪一层。

---

## 〇、三句话结论

1. **数值层可搬，公式不能照搬**。KR1 的护甲是"线性直减百分比"，MySTG 是"递减曲线"（`1 - def/(50+def)`）。直接把 KR 的 `护甲 95` 填进 MySTG 的 `Defence`，重甲单位从"物伤只剩 5%"变成"还剩 34.5%"——整个"重甲必须上炮/法师"的战术前提直接失效。等价的 Defence 值要填 **950**。
2. **塔与技能层约七成能落，靠配置就够**。MySTG 的 163 个 Buff 类 + 32 个触发器 + 16 种子弹形态，能覆盖 KR 的毒箭、减速、眩晕、缴械、自爆、召唤、溅射、连锁；真正缺的是**秒杀通道**和**护甲折半的炮伤通道**。
3. **兵营与英雄这两块，不是改造而是从零建**。MySTG 的 `CharacterGameData` 及其子类 `CharacterTowerData` **都没有 HP 字段**——怪物技能可以选中塔（`MONSTER_SEARCH_TARGET.HIGHEST_ATTACK_TOWER`），但只能给它挂缴械类 debuff，杀不死它。KR 的兵营（有血量、会被打、会复活）、英雄（可移动、可拦截、13 位 27 技能）在 MySTG 里没有任何承载体——只剩注释里的"英雄"字样。

---

## 一、四个架构级差异（先看这个，其余都是细节）

| 维度 | KR1 | MySTG | 影响 |
|---|---|---|---|
| **塔位** | 地图上固定塔位，只能在基座上建 | 自由网格，`GRID_STATE.WALKABLE` 的格子既能走也能放塔 | KR 的 27 张 1200×1000 底图带固定基座美术，直接拿来用会打架 |
| **路径** | 固定路线，怪物沿预设折线走 | **A\* 动态寻路**，放塔会改路线，甚至能堵路 | MySTG 比 KR 多一层玩法，但 KR1 的"塔位-路径-波次"三者是设计死的，搬关卡要重排 |
| **拦截** | 兵营士兵堵路、怪停下互砍，是核心控场手段 | **无拦截**。塔不阻挡，怪物穿行；唯一能影响路径的石墩是"逼怪物绕路"，不是"把怪物挡下来" | 少一整条战术轴，KR 大量关卡解法依赖它 |
| **成长轴** | 塔 4 级 + 四级二选一专精；英雄 10 级 + 3 技能；关间科技树 | 塔 `Level` × `Star` 双轴 + Rogue 词条叠层；无专精分支、无科技树 | KR 是"局外养成 + 局内选择"，MySTG 是"局内 Rogue 构筑"。两套成长哲学不同 |

> 这条差异线决定了后面所有工作量：**伤害和 Buff 是改配置，塔是加类，兵营/英雄/科技树是加子系统。**

---

## 二、伤害系统：9 种类型逐条映射

MySTG 的伤害入口只有一个：`GameUtilityHotFix.generateDamage()`（`Utility/GameUtilityHotFix.cs:16-111`），走两条分支——`元素 == NONE` 走 `mDefence`，否则走 `mAntiXxxElement`。

| # | KR1 类型 | KR1 公式 | MySTG 承载 | 差距 | 改法 |
|---|---|---|---|---|---|
| 1 | 物伤 | `dmg×(100−armor)/100` | `DAMAGE_ELEMENT.NONE` + `mDefence` → `dmg×(1−def/(50+def))` | **曲线不同**。95 甲：KR 剩 5%，MySTG 剩 34.5% | 改 `generateDamage` 第 60 行，加"线性减免"分支；或用换算表把 KR 护甲转成等价 Defence |
| 2 | 法伤 | `dmg×(100−mr)/100`，完全无视护甲 | 任一元素 + `mAntiXxxElement` → `dmg×(1−anti)` | **1:1 可直接落**，只需约定哪个元素代表"魔法" | 填表即可。建议 DARK 作魔法通道，LIGHT 留给神圣 |
| 3 | 炮伤 | `dmg×(100−armor×0.5)/100` | **无** | KR 的炮伤是"护甲折半后仍吃护甲"，MySTG 元素伤害完全绕开护甲 | 需在 `generateDamage` 增加"元素伤害也吃护甲系数"的开关字段 |
| 4 | 真伤 | `dmg`，护甲魔抗全无效 | **无** | 只有 `mImmunityPhysicDamage` 这种免疫位，没有"穿透"标记 | 加 `bypassDefence` 标记；或定义零抗性元素 |
| 5 | 毒伤 | `dmg×(100−poisonRes)/100` | `DAMAGE_ELEMENT.POISION` + `mAntiPoisonElement` | **1:1 可直接落** | 填表即可 |
| 6 | 电伤 | 结算同炮伤 | `DAMAGE_ELEMENT.LIGHTNING` + `mAntiLightningElement` | 通道不同（KR 吃半护甲，MySTG 吃电抗） | 二选一：跟随炮伤改造，或按 MySTG 元素制走 |
| 7 | 分解 | `dmg`，致死化灰无尸体 | 无 | 纯表现层 | 低优先，死亡特效标记即可 |
| 8 | 秒杀 | 跳过血量直接致死 | **无** | `grep` 全工程 0 命中 | 需加致死 API + BOSS 免疫标记（`MONSTER_STRENGTH.BOSS` 可复用） |
| 9 | 吞噬 | 秒杀 + 隐藏贴图 + **消除亡语** | 无 | 同 8，另需抑制死亡回调链 | 秒杀做完后加一个 `suppressDeathTrigger` 开关 |

### 护甲换算是非线性陷阱

要让 MySTG 的 `mDefence` 复现 KR 的减伤率，得解 `def/(50+def) = armor/100`：

| KR 护甲 | 减伤 | 等价 MySTG Defence | 直接照搬会怎样 |
|---:|---:|---:|---|
| 30 | 30% | 21 | 减伤只有 15%，护甲形同虚设 |
| 60 | 60% | 75 | 减伤降到 28% |
| 80 | 80% | 200 | 减伤降到 40% |
| 95 | 95% | **950** | 减伤只剩 34.5%，重甲完全失效 |

**结论**：如果目标是复刻 KR 手感，不要换算，直接给 `generateDamage` 加一条 KR 公式分支更省事，也更好调。

### 闪避也有语义差

MySTG 的闪避在 `generateDamage:25` 一刀切——**任何伤害都吃闪避**（含魔法、毒、炮）。
KR 的闪避是 `can dodge melee attacks`，**只有近战单体可闪**，箭矢照吃。

所以 KR 里"狼群用弓箭手不用兵营"这条战术，在 MySTG 里不成立（弓箭也会被闪）。要复刻，需在 `generateDamage` 加"按子弹类型判定是否可闪"。

---

## 三、塔体系：20 座 vs 13 座，缺的是整个家族

| KR 家族 | KR 塔（Lv1→Lv4 双分支） | MySTG 可对应 | 判断 |
|---|---|---|---|
| 箭塔 | 弓兵→射手→神射手 → 游侠匿所 / 火枪要塞 | 十字弓、天空之矛、飞镖发射塔 | 能力覆盖，但 MySTG 是 13 座并列塔，没有"同一塔升级变形" |
| 法师塔 | 法师→术士→巫师 → 奥术法师 / 魔法师 | 镶嵌塔、波动塔 | 覆盖（镶嵌塔紫宝石 = 多目标法伤） |
| 炮塔 | 矮人炮塔→火炮→榴弹炮 → 大贝莎 / 特斯拉 | 火炮塔、球形飞弹塔、霰弹塔、气球炸弹塔、震荡塔 | 覆盖度高，特斯拉≈震荡塔/连线子弹 |
| **兵营** | 民兵→步兵→骑士 → 神圣秩序 / 蛮族大殿 | **无** | 缺口。石墩（`STONE_TOWER`）无攻击、无技能，作用是"改敌人行进路径 + 放战斗道具"，本质是**堵路**不是**拦截**——敌人绕开它，而不是停下来和它打 |

`TowerRegister.cs` 里只注册了 2 个特殊塔类（石墩、气球炸弹塔），其余 11 座共用 `CharacterTower` + 技能类差异化。要加兵营，需要新增 `CharacterTowerBarracks`（含士兵实体、血量、复活、拦截判定），并改动 `COMMonsterMovement` 让怪物可被"拦停"。

### 升级模型的两处硬差异

- KR：`Lv1-Lv3 面板成长 → Lv4 二选一专精`，18 个技能全绑在四级塔上。
- MySTG：`Tower` 表单行 = 一座塔，`Level` 与 `Star` 两个消耗轴（`LevelUpCost*` / `StarUpCost*`），技能是塔的固定 `Skill` 列表，**不随等级解锁**。

→ 想做"四级二选一"，现有表结构装不下，需要新增一张 `TowerUpgradeBranch`（源塔 ID → 目标塔 ID + 条件 + UI 位置）。

---

## 四、技能体系：47 个技能按"承载方式"分四类

| 承载方式 | KR1 技能（举例） | MySTG 落点 | 成本 |
|---|---|---|---|
| **纯配置可落**（约 18 个） | 毒箭、多重战斧、勇气之盾、连锁闪电、过载、治疗之光 | `BuffDetail` + `Buff` 命中挂载 | S，只填表 |
| **复用子弹形态**（约 10 个） | 霰弹、群簇轰炸、投石弹幕、寒冰风暴、奥术风暴、弹射锯片 | `SkillBulletNoMoveFan` / `Parabola` / `TrackBounce` / `CurveMultiDamage` | S~M，可能要小改参数 |
| **需新技能类**（约 12 个） | 森林之怒（范围缠绕）、传送（击退）、永恒冻土（地面减速场）、焦油炸弹（地面区域）、布雷专家（地雷） | `TowerSkill_*` 新增子类 + 地面区域实体 | M，每个半天量级 |
| **现有体系无法表达**（约 7 个） | 狙击、死亡射线、伐伐伐木、灭魂斩、变形术（都要秒杀）；野性呼唤/先祖召唤/召唤元素（友方召唤）；巨熊形态/钢铁时间（完全无敌） | 秒杀通道缺失、友方召唤缺失、全免疫缺失 | L，需先补底层 |

### 三个具体缺口（都是底层，不是单个技能）

1. **秒杀**：KR 有 5 个我方秒杀技能，且区分"是否禁用亡语"（死亡射线禁用，狙击不禁用）。MySTG 零实现。
2. **友方召唤**：MySTG 有 `BuffSummonMonster` 和 `CmdGlobalCreateMonster`，只能召**敌方**。塔召唤友军士兵/元素/野猫无承载。
3. **完全无敌**：只有 `BuffImmunityPhysicDamage`（免疫物伤）和 `BuffImmunityElementDebuffDamage`（免疫元素 debuff 伤害），拼不出"免疫全部 6 种"。KR 的巨熊形态、钢铁时间依赖它。

> 好消息：MySTG 的 `BuffPushMove`（推动）、`BuffFreeze`、`BuffParalysis`、`BuffConfusion`、`BuffDisarm`、`BuffMoveSpeedDown*`（5 个变体）已经覆盖 KR 的击退、冰冻、眩晕、混乱、缴械、减速六类控场。KR 的控制技能基本不用写新代码。

---

## 五、敌人：70 个单位的机制覆盖度

| KR 机制 | 出现频率 | MySTG 承载 | 差距 |
|---|---|---|---|
| 护甲 / 魔抗 | 17 个有甲、21 个有魔抗 | `Defence` / `mAntiXxxElement` | 公式差异见第二节；魔抗可直接落 |
| 飞行 | 石像鬼、火箭骑兵、魔精 | 双套寻路（四方向/六方向）+ `FLY_ONLY_HIDE` 格 | **完整支持** |
| 闪避近战 | 土狼/强盗/座狼/冬狼/恶魔猎犬 | `mEvasion`（全局闪避） | 语义差，见第二节 |
| 免疫中毒 | 11 个单位 | `mAntiPoisonElement = 1` 或 `BuffImmunityElementDebuffDamage` | 可直接落 |
| 死亡自爆 | 恶魔之子/猎犬/头领、憎恶、哥布林破坏者、腐烂魔菇 | `MonsterPassiveSkill_SelfExplosion` | **完整支持** |
| 死亡召唤/分裂 | 蜘蛛主母、座狼骑兵、堕落骑士、蘑菇人 | `BuffSummonMonster` + `DeadCanTrigger` | 大概率可落，需验证死亡时触发链 |
| 自我治疗 | 巨魔系、萨满（治他人） | Buff 回血族 | 可落 |
| 召唤物（傀儡头/幼蛛） | 毁灭者、蘑菇人 | `CmdGlobalCreateMonster` | **完整支持** |
| 4 次伤害护盾 | 恶魔头领 | 无"次数护盾"类 Buff | 需新增一个 Buff |
| 秒杀 / 变羊免疫 | BOSS + 地狱犬 | 无秒杀，故无需免疫 | 随秒杀一起做 |
| 吞噬（BOSS 反制） | 萨雷格兹、大雪怪 | 无 | 随秒杀一起做 |

**可直接复用的一项**：`Monster` 表已有 `Population`（人口/代价）、`Score`（分数）、`WaveExp`、`HurtHp`（到达终点扣血）、`HpStar/SpeedStar/DefenceStar`（星级成长系数）——KR 的"代价 / 赏金 / 漏怪扣心"三个字段全有对应，数值可直接灌。

---

## 六、缺口清单（按改造成本分档）

### S 档：只填配置，不动代码

- 法伤 / 毒伤 / 电伤三通道（元素抗性 1:1）
- 16 种 Buff 型塔技能（毒箭、多重战斧、勇气之盾、过载、连锁闪电…）
- 70 个敌人的 HP / 移速 / 抗性 / 人口 / 赏金（护甲值除外）
- 飞行、免疫中毒、死亡自爆、召唤、自我治疗
- 六类控场（冰冻、麻痹、眩晕、混乱、缴械、减速）

### M 档：加类或改一个函数

| 改造点 | 位置 | 规模 |
|---|---|---|
| 线性护甲减免分支 | `GameUtilityHotFix.generateDamage:60` | 1 个函数 |
| 炮伤"护甲折半"通道 | 同上 + `SkillBullet` 加字段 | 1 个字段 + 1 个分支 |
| 真伤穿透标记 | 同上 | 同 |
| 闪避按弹种判定 | `generateDamage:25` | 1 个条件 |
| 地面区域类技能（焦油/冻土/地雷/焦土） | 新增 `TowerSkill_GroundArea` + 区域实体 | 1 个技能类 |
| 范围控制技能（缠绕/传送） | 新增 `TowerSkill_AoeControl` | 1 个技能类 |
| 次数护盾 Buff | 新增 `BuffDamageCountShield` | 1 个 Buff 类 |
| 塔四级分支专精 | 新增 `TowerUpgradeBranch` 表 + UI | 1 张表 + 1 个界面 |

### L 档：新增子系统

| 子系统 | 为什么是 L | 依赖 |
|---|---|---|
| **拦截 / 兵营** | `CharacterGameData` 无 HP，塔不可被攻击；怪物路径逻辑里没有"被拦停"状态；需要士兵实体 + 血条 + 复活 + 仇恨 | 改 `CharacterGameData`、`COMMonsterMovement`、新增 `CharacterTowerBarracks` + `CharacterSoldier` |
| **英雄** | 无 `CharacterHero` 类，只剩注释残留；需可移动单位 + 技能栏 + 等级 + 局内复活 | 拦截做完后才能做（英雄本质是"可移动的兵营单位"） |
| **科技树** | MySTG 存档只有通关星级，无局外成长结构 | `COMClientUserData` 扩展 + 一颗 UI 树 |
| **秒杀通道** | 需致死 API + 亡语抑制 + BOSS 免疫 + 5 个技能改造 | 相对独立，可先做 |

---

## 七、如果要做，建议的顺序

1. **先修伤害公式**（半天）。这是所有数值的地基，不修则 KR 数值全部失真。加线性护甲分支 + 炮伤折半 + 真伤穿透三个开关，用 `SkillBullet` 上的枚举区分。
2. **再灌配置验证手感**（S 档全量）。用现有 13 塔对上 KR 的箭/法/炮三个家族，跑一张 KR 地图，验证"重甲上炮"是否成立。这一步能验证 70% 的可行性假设。
3. **补两个技能类**（M 档）。地面区域 + 范围控制。做完这两类，KR 的 47 个技能能覆盖到约 40 个。
4. **最后才碰兵营**。这是唯一需要动 `COMMonsterMovement` 和 `CharacterGameData` 的改造，风险最高，收益也最高（KR 一半的关卡解法靠它）。

> 不建议一上来做英雄或科技树。它们都建在拦截之上，没有拦截，英雄只是"会走路的塔"。

---

## 八、一句话可行性

**做"KR 风格的塔 + 技能 + 数值"，MySTG 是合适的地基，七成靠配置；做"KR 原样复刻"，卡在兵营拦截这一层，需要动核心角色数据结构和寻路逻辑——这不是配置能解决的，是 1/3 个战斗系统的重写。**

---

## 附：三处可直接引用的对照

| 对照项 | KR1 | MySTG | 位置 |
|---|---|---|---|
| 伤害入口 | `damage(type, armor, mr)` | `generateDamage(target, attacker, bullet, out isHit, out isCritical, out deltaType)` | `GameUtilityHotFix.cs:16` |
| 敌人字段 | HP / 伤害 / 攻速 / 护甲 / 魔抗 / 移速 / 代价 / 赏金 | HP / Defence / Speed / 6 抗性 / Evasion / Population / Score | `Excels/Monster.csv` |
| 塔字段 | 建造费 / 总造价 / 伤害 / 攻速 / 类型 / 射程 | Price / LevelUpCost / StarUpCost / Skill / Range / HexRange | `Excels/Tower.csv` |
