// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Buff表格
public class EDBuff : ExcelDataT<EDBuff>
{
	public const int BuffPoison_ID = 3;				// 中毒
	public const int BuffMoveSpeedDown_ID = 4;		// 百分比减速,固定百分比
	public const int BuffMoveSpeedDownByLevel_ID = 5;// 百分比减速,根据宝石等级和塔等级计算
	public const int BuffSkillRangeUp_ID = 6;		// 增加射程,固定百分比
	public const int BuffStrickBack_ID = 7;			// 击退一定距离,单位为格子大小
	public const int TriggerHit_ID = 9;				// 命中时触发
	public const int BuffDisableSkill_ID = 11;		// 不允许释放技能
	public const int TriggerHPUnderPercent_ID = 12;	// 血量降低到一定时触发
	public const int BuffMoveSpeedUp_ID = 13;		// 百分比加速,固定百分比
	public const int BuffFlyable_ID = 14;			// 具有飞天能力,可以使用飞行路线
	public const int BuffFlashForward_ID = 15;		// 向前闪现一定距离
	public const int BuffSummonMonster_ID = 16;		// 召唤怪物
	public const int TriggerHPUnderPercentMulti_ID = 20;// 血量降低到一定时触发多个buff
	public const int BuffDamageOnce_ID = 21;		// 单次固定伤害
	public const int BuffHoldPosition_ID = 22;		// 禁锢,不允许移动
	public const int BuffBuilding_ID = 26;			// 塔建造中的状态
	public const int BuffTypeTowerAttackUp_ID = 27;	// (已废弃)提升所有指定类型塔的攻击力
	public const int BuffVertigo_ID = 28;			// 眩晕
	public const int BuffHasTypeBuffIncreaseDamage_ID = 29;// 攻击拥有指定buff类型的敌人时伤害增加
	public const int TriggerWillHit_ID = 30;		// 即将命中时触发
	public const int BuffCriticalUp_ID = 31;		// 暴击率增加
	public const int BuffTypeTowerIncreaseSelfCritical_ID = 32;// 场上指定类型的塔越多,自身增加的暴击率越多
	public const int BuffTypeTowerIncreaseSelfDamage_ID = 33;// 自己的暴击不可被闪避
	public const int BuffBurn_ID = 36;				// 场上指定类型的塔越多,伤害增加越多
	public const int BuffFireImprint_ID = 37;		// 火焰印记
	public const int BuffBeenFireDamageUp_ID = 41;	// 受到的火属性伤害提升
	public const int BuffBeenDarkDamageUp_ID = 42;	// 受到的暗属性伤害提升
	public const int BuffBeenIceDamageUp_ID = 43;	// 受到的冰属性伤害提升
	public const int BuffBeenLightDamageUp_ID = 44;	// 受到的光属性伤害提升
	public const int BuffBeenPoisonDamageUp_ID = 45;// 受到的毒属性伤害提升
	public const int BuffBeenLightningDamageUp_ID = 46;// 受到的电属性伤害提升
	public const int BuffBeenFireDamageDown_ID = 47;// 受到的火属性伤害降低
	public const int BuffBeenDarkDamageDown_ID = 48;// 受到的暗属性伤害降低
	public const int BuffBeenIceDamageDown_ID = 49;	// 受到的冰属性伤害降低
	public const int BuffBeenLightDamageDown_ID = 50;// 受到的光属性伤害降低
	public const int BuffBeenPoisonDamageDown_ID = 51;// 受到的毒属性伤害降低
	public const int BuffBeenLightningDamageDown_ID = 52;// 受到的电属性伤害降低
	public const int BuffAttackUp_ID = 53;			// 提升攻击力
	public const int BuffAttackDown_ID = 54;		// 降低攻击力
	public const int TriggerDirectly_ID = 55;		// 直接触发
	public const int BuffBleeding_ID = 56;			// 出血
	public const int BuffTypeBuffIncreaseSelfAttack_ID = 57;// 场上拥有指定状态的敌人越多,自身增加的攻击力越多
	public const int BuffTypeBuffIncreaseSelfAttackSpeed_ID = 58;// 场上拥有指定状态的敌人越多,自身减少的攻击速度越多
	public const int BuffRangeEnemyBurnToIncrease_ID = 59;// 范围内敌人身上的燃烧伤害由递减改为递增
	public const int BuffConfusion_ID = 61;			// 混乱,向路线的反方向移动
	public const int TriggerCritical_ID = 62;		// 暴击时触发
	public const int BuffAttackSpeedUp_ID = 63;		// 增加攻速
	public const int BuffTransferBack_ID = 65;		// 沿移动路径向后传送一定距离
	public const int BuffRemoveTypeBuff_ID = 66;	// 移除指定类型的buff
	public const int BuffBeenDamageDown_ID = 67;	// 受到的伤害降低
	public const int BuffBeenDamageUp_ID = 68;		// 受到的伤害提升
	public const int TriggerBuffToTypeBuffMonster_ID = 69;// 范围内拥有指定buff的敌人会附加指定buff,敌人超出范围时,会移除buff
	public const int TriggerBuffToInvisibleMonster_ID = 70;// 范围内隐身的敌人会附加指定buff,敌人超出范围时,会移除buff
	public const int BuffFloatToAir_ID = 71;		// 浮空
	public const int TriggerTypeTowerOverCountBuffToSelf_ID = 73;// 场上指定类型的塔超过一定数量时,会给自己附加指定buff,塔数量低于一定数量时会移除buff
	public const int TriggerBuffToTypeTowerAndSelf_ID = 75;// 给自己和指定类型的塔附加buff
	public const int TriggerBuffToTypeTower_ID = 76;// 给指定类型的塔附加buff
	public const int BuffBeenBurnDamageUp_ID = 77;	// 受到的燃烧伤害提升
	public const int BuffBeenPoisoningDamageUp_ID = 78;// 受到的中毒伤害提升
	public const int BuffBeenAllElementDamageDown_ID = 79;// 受到的所有元素属性伤害降低
	public const int BuffSneak_ID = 80;				// 潜行
	public const int BuffVertigoDecrease_ID = 81;	// 降低被眩晕的概率和眩晕的时间
	public const int TriggerWillDie_ID = 82;		// 即将死亡时触发
	public const int BuffAntiStrickBack_ID = 83;	// 无法被击退
	public const int BuffDisarm_ID = 85;			// 缴械
	public const int BuffAntiCriticalUp_ID = 86;	// 暴击抗性提升
	public const int BuffBeenShockedDamageUp_ID = 87;// 受到的感电伤害提升
	public const int TriggerTypeMonsterDie_ID = 88;	// 指定怪物死亡时触发
	public const int BuffSlowIncrease_ID = 89;		// 受到的减速效果提升
	public const int BuffImmunityElementDebuffDamage_ID = 91;// 免疫所有元素伤害
	public const int BuffClearDebuff_ID = 92;		// 清除所有debuff
	public const int TriggerBuffToRangeMonsterOnce_ID = 93;// 范围内所有怪物附加buff,只在进入状态时添加一次,并且不会主动移除
	public const int BuffRecoverHPPercentOnce_ID = 94;// 百分比恢复血量
	public const int BuffEvasionUp_ID = 95;			// 闪避率提升
	public const int BuffForceChangeTarget_ID = 97;	// 强制改变防御塔和英雄的目标选择为指定目标,技能目标为对自己释放的除外
	public const int BuffShocked_ID = 98;			// 感电
	public const int BuffFreeze_ID = 99;			// 冰冻
	public const int BuffParalysis_ID = 100;		// 麻痹
	public const int BuffMoveSpeedDownValue_ID = 101;// 固定数值减速,减速到负数时,可以让怪物后退
	public const int BuffPushMove_ID = 102;			// 怪物在眩晕等无法移动的状态时，强制移动怪物位置
	public const int BuffMoveSpeedUpValue_ID = 103;	// 固定数值加速
	public const int BuffDamageOverTime_ID = 104;	// 无属性持续伤害
	public const int BuffMoveSpeedDownNoEffect_ID = 105;// 百分比减速,固定百分比，无特效
	public const int BuffInTunnel_ID = 106;			// 通过隧道时不可攻击状态
	public const int TriggerBuffToAllTower_ID = 107;// 给所有防御塔附加buff,持续时间内新增的塔也会被附加buff
	public const int BuffDamageUp_ID = 108;			// 伤害增加
	public const int BuffTowerExploRangeUp_ID = 109;// 增加子弹爆炸范围
	public const int BuffZhenDangTaBulletCountUp_ID = 110;// 增加电磁震荡塔能量球个数
	public const int BuffResetLastSkillCD_ID = 111;	// 重置刚释放过的技能CD
	public const int TriggerFireSkill_ID = 112;		// 释放技能时触发
	public const int TriggerFireCountSkillHit_ID = 113;// 释放一定次数技能后的命中时触发
	public const int BuffRangeDamageOnce_ID = 114;	// 造成一次范围攻击力百分比伤害
	public const int TriggerFireCountSkillWillGenerateDamage_ID = 115;// 释放一定次数技能后的即将命中时触发
	public const int BuffAlwaysCriticalHit_ID = 116;// 攻击必定暴击
	public const int BuffTypeTowerIncreaseSelfAttack_ID = 117;// 场上指定类型的塔越多,自身增加的攻击力越多
	public const int BuffTypeTowerIncreaseSelfAttackSpeed_ID = 118;// 场上指定类型的塔越多,自身增加的攻速越多
	public const int BuffDamageUpBeforeWave_ID = 119;// 前一定波数伤害增加
	public const int BuffIncreaseBulletCount_ID = 121;// 增加子弹数量
	public const int BuffAttackSpeedDown_ID = 122;	// 降低攻速
	public const int BuffSkillRangeDown_ID = 123;	// 射程降低
	public const int TriggerKillMonster_ID = 124;	// 击杀怪物时触发
	public const int TriggerTimeInterval_ID = 125;	// 每隔一定时间触发一次
	public const int BuffBulletFlyDisIncreaseExploRange_ID = 126;// 爆炸范围随着飞行距离增加
	public const int BuffBulletFlyDisIncreaseDamage_ID = 127;// 伤害随着飞行距离增加
	public const int BuffBulletDamageUpInExploRange_ID = 128;// 对子弹爆炸一定范围内的敌人伤害增加
	public const int BuffBulletSpeedUp_ID = 129;	// 子弹飞行速度增加
	public const int TriggerBuffToBulletExploRange_ID = 130;// 子弹爆炸时给范围内所有敌人附加buff
	public const int BuffXiangQianTaPurpleGemTargetCountUp_ID = 132;// 镶嵌塔紫宝石技能弹射目标数量增加
	public const int BuffTowerExploRangeDown_ID = 133;// 减少火炮塔爆炸范围
	public const int BuffIncreaseBulletCountHuoPao_ID = 134;// 火炮塔子弹数量增加
	public const int BuffDamageDown_ID = 135;		// 伤害降低
	public const int BuffAroundTowerCountAttackUp_ID = 136;// 塔附近1格有n个塔，每个塔增加该塔的攻击
	public const int BuffAroundTowerCountAttackSpeedUp_ID = 137;// 塔附近1格有n个塔，每个塔增加该塔的攻速
	public const int BuffMoveSpeedDownRouge_ID = 138;// 肉鸽模式球形飞弹天赋特定减速Buff,百分比
	public const int TriggerWillHitHpMinPercent_ID = 139;// 击中前怪物血量高于百分比触发
	public const int BuffDefenceDownPercent_ID = 140;// 防御力百分比降低
	public const int BuffInstantDeath_ID = 141;		// 即死
	public const int TriggerWillHitHpMaxPercent_ID = 142;// 击中前怪物血量低于百分比触发
	public const int BuffDamageUpStrengthMonster_ID = 143;// 攻击指定怪物时攻击力百分比加成
	public const int BuffAttackSpeedUpStepped_ID = 144;// 初始攻击间隔提高，每次攻击降低攻击间隔，有叠加上限，若一定时间内未进行攻击则重置
	public const int TriggerBuffOnWaveStart_ID = 145;// 每波开始，概率添加某些状态，没随机到就会移除
	public const int BuffCriticalDamageUp_ID = 146;	// 暴击伤害增加
	public const int BuffIncreaseBulletCountByNoDamageTime_ID = 147;// 一段时间内没有造成伤害，下次攻击子弹增加
	public const int BuffRangeDamageByHpMaxPercent_ID = 148;// 击杀怪物时,对周围指定类型的怪物造成当前怪物最大血量百分比的伤害
	public const int BuffIncreaseBulletCountPercent_ID = 149;// 百分比增加子弹数量
	public const int TriggerBuffByWaveBulletCount_ID = 150;// 塔释放一定数量子弹,触发buff,直到波次结束
	public const int BuffRogueKillMonsterAddBuildCoin_ID = 151;// 肉鸽模式，塔每击杀n个敌人，获得m肉鸽建造点
	public const int BuffAttackUpOnceByKillMonster_ID = 152;// 击杀n个敌人后，下一次攻击提高
	public const int BuffRogueKillMonsterCureLevelHp_ID = 153;// 每击杀n个敌人，回复m点已损失的羊村生命
	public const int BuffAttackDownThenUpByHitSameMonster_ID = 154;// 攻击力降低，每次攻击提高攻击力。上限n层，切换目标时重置
	public const int BuffChangeSearchTargetType_ID = 155;// 寻敌方式修改
	public const int BuffDamageUpToDebuffMonster_ID = 156;// 对处于异常状态下的单位造成的伤害提升
	public const int BuffRogueKillMonsterFreeUpLevel_ID = 157;// 击杀n个敌人后，该塔升级免费
	public const int BuffScaleBullet_ID = 158;		// 调整子弹大小
	public const int BuffBulletAttackUpHitMonster_ID = 159;// 波动塔子弹每穿过一个敌方单位，子弹攻击提高
	public const int BuffIncreaseFlyDisByRogueTowerLevel_ID = 160;// 肉鸽模式，按塔等级增加子弹飞行距离
	public const int BuffIncreaseBulletBounceTimes_ID = 161;// 增加弹跳子弹的弹跳次数
	public const int BuffKillMonsterChangeRandomTower_ID = 162;// 击杀n个敌人后，随机变成一个塔，保留等级
	public const int BuffIncreaseHuoPaoExplosionMulti_ID = 163;// 火炮子弹概率爆炸多次
	public const int BuffZhenDangNotDestroyBulletOnHit_ID = 164;// 设置电磁震荡塔子弹不消失
	public const int TriggerBuffByEnterTowerRange_ID = 165;// 向进入该塔射程范围内的怪物添加buff
	public const int BuffMoveSpeedDownZhenDownTowerRange_ID = 166;// 电磁震荡塔射程减速buff，不叠加
	public const int BuffZhenDangAddBulletByKillMonster_ID = 167;// 电磁震荡塔击杀n个敌人，获得能量球
	public const int TriggerBuffWhenBulletConsume_ID = 168;// 消耗n个子弹后触发buff
	public const int TriggerBuffToHexRangeTower_ID = 169;// 对六边形半径范围内的塔触发buff
	public const int BuffZhenDangConsumeCriticalUp_ID = 170;// 电磁震荡塔子弹消耗的特殊暴击加成buff
	public const int TriggerBuffGlobalWhenBulletExplosionHuoPao_ID = 171;// 当塔的子弹爆炸时，对战斗中的全局角色触发buff
	public const int TriggerBuffWithAreaCollider_ID = 172;// 生成一个区域模型，根据他的碰撞箱，对其中的怪物附加buff
	public const int BuffMoveSpeedDownHuoPaoExplosionArea_ID = 173;// 火炮塔子弹爆炸残留提供的减速buff
	public const int BuffFocusAttackMonster_ID = 174;// 集火怪物的buff
	public const int TriggerBuffToGridRangeTowerWhenPlace_ID = 175;// 在放置和移动英雄时,对一定范围内的塔触发buff
	public const int BuffWaveUpTowerNear_ID = 176;	// 每过n回合，对旁边的防御塔升级
	public const int BuffAttackSpeedUpGongJianShou_ID = 177;// 弓手特殊攻速增加
	public const int BuffRogueHitMonsterAddBuildCoin_ID = 178;// 每击中n个敌人，获得m银币
	public const int BuffRogueCoinInterest_ID = 179;// 肉鸽回合结束时，每有n银币，额外获得m银币，m有上限
	public const int BuffRogueMonsterBreakAddCoin_ID = 180;// 肉鸽模式前n个进入基地的怪物转化为m银币，boss无效
	public const int BuffImmunityPhysicDamage_ID = 183;// 免疫物理伤害
	public const int BuffAttackSpeedUpByKillMonster_ID = 204;// 每击杀n单位，攻速增加
	public const int BuffAttackCriticalUpByTowerCount_ID = 205;// 根据场上防御塔数量，提升自身攻击力和暴击率
	public const int TriggerBuffToGridRangeTowerByTowerCount_ID = 206;// 在放置和移动英雄时，且有一定数量的某种塔时，对一定范围内的塔触发buff
	public const int BuffSpeedAttackUpByTowerCount_ID = 207;// 根据场上某种防御塔数量，提升自身攻速和攻击
	public const int BuffAttackUpByGridTowerCount_ID = 208;// 附近n格内有m个塔时，提升攻击力
	public const int BuffAddTowerRogue_ID = 500;	// 将防御塔带入肉鸽战斗

	public static EDBuff _BuffPoison;				// 中毒
	public static EDBuff _BuffMoveSpeedDown;		// 百分比减速,固定百分比
	public static EDBuff _BuffMoveSpeedDownByLevel;	// 百分比减速,根据宝石等级和塔等级计算
	public static EDBuff _BuffSkillRangeUp;			// 增加射程,固定百分比
	public static EDBuff _BuffStrickBack;			// 击退一定距离,单位为格子大小
	public static EDBuff _TriggerHit;				// 命中时触发
	public static EDBuff _BuffDisableSkill;			// 不允许释放技能
	public static EDBuff _TriggerHPUnderPercent;	// 血量降低到一定时触发
	public static EDBuff _BuffMoveSpeedUp;			// 百分比加速,固定百分比
	public static EDBuff _BuffFlyable;				// 具有飞天能力,可以使用飞行路线
	public static EDBuff _BuffFlashForward;			// 向前闪现一定距离
	public static EDBuff _BuffSummonMonster;		// 召唤怪物
	public static EDBuff _TriggerHPUnderPercentMulti;// 血量降低到一定时触发多个buff
	public static EDBuff _BuffDamageOnce;			// 单次固定伤害
	public static EDBuff _BuffHoldPosition;			// 禁锢,不允许移动
	public static EDBuff _BuffBuilding;				// 塔建造中的状态
	public static EDBuff _BuffTypeTowerAttackUp;	// (已废弃)提升所有指定类型塔的攻击力
	public static EDBuff _BuffVertigo;				// 眩晕
	public static EDBuff _BuffHasTypeBuffIncreaseDamage;// 攻击拥有指定buff类型的敌人时伤害增加
	public static EDBuff _TriggerWillHit;			// 即将命中时触发
	public static EDBuff _BuffCriticalUp;			// 暴击率增加
	public static EDBuff _BuffTypeTowerIncreaseSelfCritical;// 场上指定类型的塔越多,自身增加的暴击率越多
	public static EDBuff _BuffTypeTowerIncreaseSelfDamage;// 自己的暴击不可被闪避
	public static EDBuff _BuffBurn;					// 场上指定类型的塔越多,伤害增加越多
	public static EDBuff _BuffFireImprint;			// 火焰印记
	public static EDBuff _BuffBeenFireDamageUp;		// 受到的火属性伤害提升
	public static EDBuff _BuffBeenDarkDamageUp;		// 受到的暗属性伤害提升
	public static EDBuff _BuffBeenIceDamageUp;		// 受到的冰属性伤害提升
	public static EDBuff _BuffBeenLightDamageUp;	// 受到的光属性伤害提升
	public static EDBuff _BuffBeenPoisonDamageUp;	// 受到的毒属性伤害提升
	public static EDBuff _BuffBeenLightningDamageUp;// 受到的电属性伤害提升
	public static EDBuff _BuffBeenFireDamageDown;	// 受到的火属性伤害降低
	public static EDBuff _BuffBeenDarkDamageDown;	// 受到的暗属性伤害降低
	public static EDBuff _BuffBeenIceDamageDown;	// 受到的冰属性伤害降低
	public static EDBuff _BuffBeenLightDamageDown;	// 受到的光属性伤害降低
	public static EDBuff _BuffBeenPoisonDamageDown;	// 受到的毒属性伤害降低
	public static EDBuff _BuffBeenLightningDamageDown;// 受到的电属性伤害降低
	public static EDBuff _BuffAttackUp;				// 提升攻击力
	public static EDBuff _BuffAttackDown;			// 降低攻击力
	public static EDBuff _TriggerDirectly;			// 直接触发
	public static EDBuff _BuffBleeding;				// 出血
	public static EDBuff _BuffTypeBuffIncreaseSelfAttack;// 场上拥有指定状态的敌人越多,自身增加的攻击力越多
	public static EDBuff _BuffTypeBuffIncreaseSelfAttackSpeed;// 场上拥有指定状态的敌人越多,自身减少的攻击速度越多
	public static EDBuff _BuffRangeEnemyBurnToIncrease;// 范围内敌人身上的燃烧伤害由递减改为递增
	public static EDBuff _BuffConfusion;			// 混乱,向路线的反方向移动
	public static EDBuff _TriggerCritical;			// 暴击时触发
	public static EDBuff _BuffAttackSpeedUp;		// 增加攻速
	public static EDBuff _BuffTransferBack;			// 沿移动路径向后传送一定距离
	public static EDBuff _BuffRemoveTypeBuff;		// 移除指定类型的buff
	public static EDBuff _BuffBeenDamageDown;		// 受到的伤害降低
	public static EDBuff _BuffBeenDamageUp;			// 受到的伤害提升
	public static EDBuff _TriggerBuffToTypeBuffMonster;// 范围内拥有指定buff的敌人会附加指定buff,敌人超出范围时,会移除buff
	public static EDBuff _TriggerBuffToInvisibleMonster;// 范围内隐身的敌人会附加指定buff,敌人超出范围时,会移除buff
	public static EDBuff _BuffFloatToAir;			// 浮空
	public static EDBuff _TriggerTypeTowerOverCountBuffToSelf;// 场上指定类型的塔超过一定数量时,会给自己附加指定buff,塔数量低于一定数量时会移除buff
	public static EDBuff _TriggerBuffToTypeTowerAndSelf;// 给自己和指定类型的塔附加buff
	public static EDBuff _TriggerBuffToTypeTower;	// 给指定类型的塔附加buff
	public static EDBuff _BuffBeenBurnDamageUp;		// 受到的燃烧伤害提升
	public static EDBuff _BuffBeenPoisoningDamageUp;// 受到的中毒伤害提升
	public static EDBuff _BuffBeenAllElementDamageDown;// 受到的所有元素属性伤害降低
	public static EDBuff _BuffSneak;				// 潜行
	public static EDBuff _BuffVertigoDecrease;		// 降低被眩晕的概率和眩晕的时间
	public static EDBuff _TriggerWillDie;			// 即将死亡时触发
	public static EDBuff _BuffAntiStrickBack;		// 无法被击退
	public static EDBuff _BuffDisarm;				// 缴械
	public static EDBuff _BuffAntiCriticalUp;		// 暴击抗性提升
	public static EDBuff _BuffBeenShockedDamageUp;	// 受到的感电伤害提升
	public static EDBuff _TriggerTypeMonsterDie;	// 指定怪物死亡时触发
	public static EDBuff _BuffSlowIncrease;			// 受到的减速效果提升
	public static EDBuff _BuffImmunityElementDebuffDamage;// 免疫所有元素伤害
	public static EDBuff _BuffClearDebuff;			// 清除所有debuff
	public static EDBuff _TriggerBuffToRangeMonsterOnce;// 范围内所有怪物附加buff,只在进入状态时添加一次,并且不会主动移除
	public static EDBuff _BuffRecoverHPPercentOnce;	// 百分比恢复血量
	public static EDBuff _BuffEvasionUp;			// 闪避率提升
	public static EDBuff _BuffForceChangeTarget;	// 强制改变防御塔和英雄的目标选择为指定目标,技能目标为对自己释放的除外
	public static EDBuff _BuffShocked;				// 感电
	public static EDBuff _BuffFreeze;				// 冰冻
	public static EDBuff _BuffParalysis;			// 麻痹
	public static EDBuff _BuffMoveSpeedDownValue;	// 固定数值减速,减速到负数时,可以让怪物后退
	public static EDBuff _BuffPushMove;				// 怪物在眩晕等无法移动的状态时，强制移动怪物位置
	public static EDBuff _BuffMoveSpeedUpValue;		// 固定数值加速
	public static EDBuff _BuffDamageOverTime;		// 无属性持续伤害
	public static EDBuff _BuffMoveSpeedDownNoEffect;// 百分比减速,固定百分比，无特效
	public static EDBuff _BuffInTunnel;				// 通过隧道时不可攻击状态
	public static EDBuff _TriggerBuffToAllTower;	// 给所有防御塔附加buff,持续时间内新增的塔也会被附加buff
	public static EDBuff _BuffDamageUp;				// 伤害增加
	public static EDBuff _BuffTowerExploRangeUp;	// 增加子弹爆炸范围
	public static EDBuff _BuffZhenDangTaBulletCountUp;// 增加电磁震荡塔能量球个数
	public static EDBuff _BuffResetLastSkillCD;		// 重置刚释放过的技能CD
	public static EDBuff _TriggerFireSkill;			// 释放技能时触发
	public static EDBuff _TriggerFireCountSkillHit;	// 释放一定次数技能后的命中时触发
	public static EDBuff _BuffRangeDamageOnce;		// 造成一次范围攻击力百分比伤害
	public static EDBuff _TriggerFireCountSkillWillGenerateDamage;// 释放一定次数技能后的即将命中时触发
	public static EDBuff _BuffAlwaysCriticalHit;	// 攻击必定暴击
	public static EDBuff _BuffTypeTowerIncreaseSelfAttack;// 场上指定类型的塔越多,自身增加的攻击力越多
	public static EDBuff _BuffTypeTowerIncreaseSelfAttackSpeed;// 场上指定类型的塔越多,自身增加的攻速越多
	public static EDBuff _BuffDamageUpBeforeWave;	// 前一定波数伤害增加
	public static EDBuff _BuffIncreaseBulletCount;	// 增加子弹数量
	public static EDBuff _BuffAttackSpeedDown;		// 降低攻速
	public static EDBuff _BuffSkillRangeDown;		// 射程降低
	public static EDBuff _TriggerKillMonster;		// 击杀怪物时触发
	public static EDBuff _TriggerTimeInterval;		// 每隔一定时间触发一次
	public static EDBuff _BuffBulletFlyDisIncreaseExploRange;// 爆炸范围随着飞行距离增加
	public static EDBuff _BuffBulletFlyDisIncreaseDamage;// 伤害随着飞行距离增加
	public static EDBuff _BuffBulletDamageUpInExploRange;// 对子弹爆炸一定范围内的敌人伤害增加
	public static EDBuff _BuffBulletSpeedUp;		// 子弹飞行速度增加
	public static EDBuff _TriggerBuffToBulletExploRange;// 子弹爆炸时给范围内所有敌人附加buff
	public static EDBuff _BuffXiangQianTaPurpleGemTargetCountUp;// 镶嵌塔紫宝石技能弹射目标数量增加
	public static EDBuff _BuffTowerExploRangeDown;	// 减少火炮塔爆炸范围
	public static EDBuff _BuffIncreaseBulletCountHuoPao;// 火炮塔子弹数量增加
	public static EDBuff _BuffDamageDown;			// 伤害降低
	public static EDBuff _BuffAroundTowerCountAttackUp;// 塔附近1格有n个塔，每个塔增加该塔的攻击
	public static EDBuff _BuffAroundTowerCountAttackSpeedUp;// 塔附近1格有n个塔，每个塔增加该塔的攻速
	public static EDBuff _BuffMoveSpeedDownRouge;	// 肉鸽模式球形飞弹天赋特定减速Buff,百分比
	public static EDBuff _TriggerWillHitHpMinPercent;// 击中前怪物血量高于百分比触发
	public static EDBuff _BuffDefenceDownPercent;	// 防御力百分比降低
	public static EDBuff _BuffInstantDeath;			// 即死
	public static EDBuff _TriggerWillHitHpMaxPercent;// 击中前怪物血量低于百分比触发
	public static EDBuff _BuffDamageUpStrengthMonster;// 攻击指定怪物时攻击力百分比加成
	public static EDBuff _BuffAttackSpeedUpStepped;	// 初始攻击间隔提高，每次攻击降低攻击间隔，有叠加上限，若一定时间内未进行攻击则重置
	public static EDBuff _TriggerBuffOnWaveStart;	// 每波开始，概率添加某些状态，没随机到就会移除
	public static EDBuff _BuffCriticalDamageUp;		// 暴击伤害增加
	public static EDBuff _BuffIncreaseBulletCountByNoDamageTime;// 一段时间内没有造成伤害，下次攻击子弹增加
	public static EDBuff _BuffRangeDamageByHpMaxPercent;// 击杀怪物时,对周围指定类型的怪物造成当前怪物最大血量百分比的伤害
	public static EDBuff _BuffIncreaseBulletCountPercent;// 百分比增加子弹数量
	public static EDBuff _TriggerBuffByWaveBulletCount;// 塔释放一定数量子弹,触发buff,直到波次结束
	public static EDBuff _BuffRogueKillMonsterAddBuildCoin;// 肉鸽模式，塔每击杀n个敌人，获得m肉鸽建造点
	public static EDBuff _BuffAttackUpOnceByKillMonster;// 击杀n个敌人后，下一次攻击提高
	public static EDBuff _BuffRogueKillMonsterCureLevelHp;// 每击杀n个敌人，回复m点已损失的羊村生命
	public static EDBuff _BuffAttackDownThenUpByHitSameMonster;// 攻击力降低，每次攻击提高攻击力。上限n层，切换目标时重置
	public static EDBuff _BuffChangeSearchTargetType;// 寻敌方式修改
	public static EDBuff _BuffDamageUpToDebuffMonster;// 对处于异常状态下的单位造成的伤害提升
	public static EDBuff _BuffRogueKillMonsterFreeUpLevel;// 击杀n个敌人后，该塔升级免费
	public static EDBuff _BuffScaleBullet;			// 调整子弹大小
	public static EDBuff _BuffBulletAttackUpHitMonster;// 波动塔子弹每穿过一个敌方单位，子弹攻击提高
	public static EDBuff _BuffIncreaseFlyDisByRogueTowerLevel;// 肉鸽模式，按塔等级增加子弹飞行距离
	public static EDBuff _BuffIncreaseBulletBounceTimes;// 增加弹跳子弹的弹跳次数
	public static EDBuff _BuffKillMonsterChangeRandomTower;// 击杀n个敌人后，随机变成一个塔，保留等级
	public static EDBuff _BuffIncreaseHuoPaoExplosionMulti;// 火炮子弹概率爆炸多次
	public static EDBuff _BuffZhenDangNotDestroyBulletOnHit;// 设置电磁震荡塔子弹不消失
	public static EDBuff _TriggerBuffByEnterTowerRange;// 向进入该塔射程范围内的怪物添加buff
	public static EDBuff _BuffMoveSpeedDownZhenDownTowerRange;// 电磁震荡塔射程减速buff，不叠加
	public static EDBuff _BuffZhenDangAddBulletByKillMonster;// 电磁震荡塔击杀n个敌人，获得能量球
	public static EDBuff _TriggerBuffWhenBulletConsume;// 消耗n个子弹后触发buff
	public static EDBuff _TriggerBuffToHexRangeTower;// 对六边形半径范围内的塔触发buff
	public static EDBuff _BuffZhenDangConsumeCriticalUp;// 电磁震荡塔子弹消耗的特殊暴击加成buff
	public static EDBuff _TriggerBuffGlobalWhenBulletExplosionHuoPao;// 当塔的子弹爆炸时，对战斗中的全局角色触发buff
	public static EDBuff _TriggerBuffWithAreaCollider;// 生成一个区域模型，根据他的碰撞箱，对其中的怪物附加buff
	public static EDBuff _BuffMoveSpeedDownHuoPaoExplosionArea;// 火炮塔子弹爆炸残留提供的减速buff
	public static EDBuff _BuffFocusAttackMonster;	// 集火怪物的buff
	public static EDBuff _TriggerBuffToGridRangeTowerWhenPlace;// 在放置和移动英雄时,对一定范围内的塔触发buff
	public static EDBuff _BuffWaveUpTowerNear;		// 每过n回合，对旁边的防御塔升级
	public static EDBuff _BuffAttackSpeedUpGongJianShou;// 弓手特殊攻速增加
	public static EDBuff _BuffRogueHitMonsterAddBuildCoin;// 每击中n个敌人，获得m银币
	public static EDBuff _BuffRogueCoinInterest;	// 肉鸽回合结束时，每有n银币，额外获得m银币，m有上限
	public static EDBuff _BuffRogueMonsterBreakAddCoin;// 肉鸽模式前n个进入基地的怪物转化为m银币，boss无效
	public static EDBuff _BuffImmunityPhysicDamage;	// 免疫物理伤害
	public static EDBuff _BuffAttackSpeedUpByKillMonster;// 每击杀n单位，攻速增加
	public static EDBuff _BuffAttackCriticalUpByTowerCount;// 根据场上防御塔数量，提升自身攻击力和暴击率
	public static EDBuff _TriggerBuffToGridRangeTowerByTowerCount;// 在放置和移动英雄时，且有一定数量的某种塔时，对一定范围内的塔触发buff
	public static EDBuff _BuffSpeedAttackUpByTowerCount;// 根据场上某种防御塔数量，提升自身攻速和攻击
	public static EDBuff _BuffAttackUpByGridTowerCount;// 附近n格内有m个塔时，提升攻击力
	public static EDBuff _BuffAddTowerRogue;		// 将防御塔带入肉鸽战斗

	public static EDBuff BuffPoison { get { return _BuffPoison ??= mTable.query(BuffPoison_ID); } }// 中毒
	public static EDBuff BuffMoveSpeedDown { get { return _BuffMoveSpeedDown ??= mTable.query(BuffMoveSpeedDown_ID); } }// 百分比减速,固定百分比
	public static EDBuff BuffMoveSpeedDownByLevel { get { return _BuffMoveSpeedDownByLevel ??= mTable.query(BuffMoveSpeedDownByLevel_ID); } }// 百分比减速,根据宝石等级和塔等级计算
	public static EDBuff BuffSkillRangeUp { get { return _BuffSkillRangeUp ??= mTable.query(BuffSkillRangeUp_ID); } }// 增加射程,固定百分比
	public static EDBuff BuffStrickBack { get { return _BuffStrickBack ??= mTable.query(BuffStrickBack_ID); } }// 击退一定距离,单位为格子大小
	public static EDBuff TriggerHit { get { return _TriggerHit ??= mTable.query(TriggerHit_ID); } }// 命中时触发
	public static EDBuff BuffDisableSkill { get { return _BuffDisableSkill ??= mTable.query(BuffDisableSkill_ID); } }// 不允许释放技能
	public static EDBuff TriggerHPUnderPercent { get { return _TriggerHPUnderPercent ??= mTable.query(TriggerHPUnderPercent_ID); } }// 血量降低到一定时触发
	public static EDBuff BuffMoveSpeedUp { get { return _BuffMoveSpeedUp ??= mTable.query(BuffMoveSpeedUp_ID); } }// 百分比加速,固定百分比
	public static EDBuff BuffFlyable { get { return _BuffFlyable ??= mTable.query(BuffFlyable_ID); } }// 具有飞天能力,可以使用飞行路线
	public static EDBuff BuffFlashForward { get { return _BuffFlashForward ??= mTable.query(BuffFlashForward_ID); } }// 向前闪现一定距离
	public static EDBuff BuffSummonMonster { get { return _BuffSummonMonster ??= mTable.query(BuffSummonMonster_ID); } }// 召唤怪物
	public static EDBuff TriggerHPUnderPercentMulti { get { return _TriggerHPUnderPercentMulti ??= mTable.query(TriggerHPUnderPercentMulti_ID); } }// 血量降低到一定时触发多个buff
	public static EDBuff BuffDamageOnce { get { return _BuffDamageOnce ??= mTable.query(BuffDamageOnce_ID); } }// 单次固定伤害
	public static EDBuff BuffHoldPosition { get { return _BuffHoldPosition ??= mTable.query(BuffHoldPosition_ID); } }// 禁锢,不允许移动
	public static EDBuff BuffBuilding { get { return _BuffBuilding ??= mTable.query(BuffBuilding_ID); } }// 塔建造中的状态
	public static EDBuff BuffTypeTowerAttackUp { get { return _BuffTypeTowerAttackUp ??= mTable.query(BuffTypeTowerAttackUp_ID); } }// (已废弃)提升所有指定类型塔的攻击力
	public static EDBuff BuffVertigo { get { return _BuffVertigo ??= mTable.query(BuffVertigo_ID); } }// 眩晕
	public static EDBuff BuffHasTypeBuffIncreaseDamage { get { return _BuffHasTypeBuffIncreaseDamage ??= mTable.query(BuffHasTypeBuffIncreaseDamage_ID); } }// 攻击拥有指定buff类型的敌人时伤害增加
	public static EDBuff TriggerWillHit { get { return _TriggerWillHit ??= mTable.query(TriggerWillHit_ID); } }// 即将命中时触发
	public static EDBuff BuffCriticalUp { get { return _BuffCriticalUp ??= mTable.query(BuffCriticalUp_ID); } }// 暴击率增加
	public static EDBuff BuffTypeTowerIncreaseSelfCritical { get { return _BuffTypeTowerIncreaseSelfCritical ??= mTable.query(BuffTypeTowerIncreaseSelfCritical_ID); } }// 场上指定类型的塔越多,自身增加的暴击率越多
	public static EDBuff BuffTypeTowerIncreaseSelfDamage { get { return _BuffTypeTowerIncreaseSelfDamage ??= mTable.query(BuffTypeTowerIncreaseSelfDamage_ID); } }// 自己的暴击不可被闪避
	public static EDBuff BuffBurn { get { return _BuffBurn ??= mTable.query(BuffBurn_ID); } }// 场上指定类型的塔越多,伤害增加越多
	public static EDBuff BuffFireImprint { get { return _BuffFireImprint ??= mTable.query(BuffFireImprint_ID); } }// 火焰印记
	public static EDBuff BuffBeenFireDamageUp { get { return _BuffBeenFireDamageUp ??= mTable.query(BuffBeenFireDamageUp_ID); } }// 受到的火属性伤害提升
	public static EDBuff BuffBeenDarkDamageUp { get { return _BuffBeenDarkDamageUp ??= mTable.query(BuffBeenDarkDamageUp_ID); } }// 受到的暗属性伤害提升
	public static EDBuff BuffBeenIceDamageUp { get { return _BuffBeenIceDamageUp ??= mTable.query(BuffBeenIceDamageUp_ID); } }// 受到的冰属性伤害提升
	public static EDBuff BuffBeenLightDamageUp { get { return _BuffBeenLightDamageUp ??= mTable.query(BuffBeenLightDamageUp_ID); } }// 受到的光属性伤害提升
	public static EDBuff BuffBeenPoisonDamageUp { get { return _BuffBeenPoisonDamageUp ??= mTable.query(BuffBeenPoisonDamageUp_ID); } }// 受到的毒属性伤害提升
	public static EDBuff BuffBeenLightningDamageUp { get { return _BuffBeenLightningDamageUp ??= mTable.query(BuffBeenLightningDamageUp_ID); } }// 受到的电属性伤害提升
	public static EDBuff BuffBeenFireDamageDown { get { return _BuffBeenFireDamageDown ??= mTable.query(BuffBeenFireDamageDown_ID); } }// 受到的火属性伤害降低
	public static EDBuff BuffBeenDarkDamageDown { get { return _BuffBeenDarkDamageDown ??= mTable.query(BuffBeenDarkDamageDown_ID); } }// 受到的暗属性伤害降低
	public static EDBuff BuffBeenIceDamageDown { get { return _BuffBeenIceDamageDown ??= mTable.query(BuffBeenIceDamageDown_ID); } }// 受到的冰属性伤害降低
	public static EDBuff BuffBeenLightDamageDown { get { return _BuffBeenLightDamageDown ??= mTable.query(BuffBeenLightDamageDown_ID); } }// 受到的光属性伤害降低
	public static EDBuff BuffBeenPoisonDamageDown { get { return _BuffBeenPoisonDamageDown ??= mTable.query(BuffBeenPoisonDamageDown_ID); } }// 受到的毒属性伤害降低
	public static EDBuff BuffBeenLightningDamageDown { get { return _BuffBeenLightningDamageDown ??= mTable.query(BuffBeenLightningDamageDown_ID); } }// 受到的电属性伤害降低
	public static EDBuff BuffAttackUp { get { return _BuffAttackUp ??= mTable.query(BuffAttackUp_ID); } }// 提升攻击力
	public static EDBuff BuffAttackDown { get { return _BuffAttackDown ??= mTable.query(BuffAttackDown_ID); } }// 降低攻击力
	public static EDBuff TriggerDirectly { get { return _TriggerDirectly ??= mTable.query(TriggerDirectly_ID); } }// 直接触发
	public static EDBuff BuffBleeding { get { return _BuffBleeding ??= mTable.query(BuffBleeding_ID); } }// 出血
	public static EDBuff BuffTypeBuffIncreaseSelfAttack { get { return _BuffTypeBuffIncreaseSelfAttack ??= mTable.query(BuffTypeBuffIncreaseSelfAttack_ID); } }// 场上拥有指定状态的敌人越多,自身增加的攻击力越多
	public static EDBuff BuffTypeBuffIncreaseSelfAttackSpeed { get { return _BuffTypeBuffIncreaseSelfAttackSpeed ??= mTable.query(BuffTypeBuffIncreaseSelfAttackSpeed_ID); } }// 场上拥有指定状态的敌人越多,自身减少的攻击速度越多
	public static EDBuff BuffRangeEnemyBurnToIncrease { get { return _BuffRangeEnemyBurnToIncrease ??= mTable.query(BuffRangeEnemyBurnToIncrease_ID); } }// 范围内敌人身上的燃烧伤害由递减改为递增
	public static EDBuff BuffConfusion { get { return _BuffConfusion ??= mTable.query(BuffConfusion_ID); } }// 混乱,向路线的反方向移动
	public static EDBuff TriggerCritical { get { return _TriggerCritical ??= mTable.query(TriggerCritical_ID); } }// 暴击时触发
	public static EDBuff BuffAttackSpeedUp { get { return _BuffAttackSpeedUp ??= mTable.query(BuffAttackSpeedUp_ID); } }// 增加攻速
	public static EDBuff BuffTransferBack { get { return _BuffTransferBack ??= mTable.query(BuffTransferBack_ID); } }// 沿移动路径向后传送一定距离
	public static EDBuff BuffRemoveTypeBuff { get { return _BuffRemoveTypeBuff ??= mTable.query(BuffRemoveTypeBuff_ID); } }// 移除指定类型的buff
	public static EDBuff BuffBeenDamageDown { get { return _BuffBeenDamageDown ??= mTable.query(BuffBeenDamageDown_ID); } }// 受到的伤害降低
	public static EDBuff BuffBeenDamageUp { get { return _BuffBeenDamageUp ??= mTable.query(BuffBeenDamageUp_ID); } }// 受到的伤害提升
	public static EDBuff TriggerBuffToTypeBuffMonster { get { return _TriggerBuffToTypeBuffMonster ??= mTable.query(TriggerBuffToTypeBuffMonster_ID); } }// 范围内拥有指定buff的敌人会附加指定buff,敌人超出范围时,会移除buff
	public static EDBuff TriggerBuffToInvisibleMonster { get { return _TriggerBuffToInvisibleMonster ??= mTable.query(TriggerBuffToInvisibleMonster_ID); } }// 范围内隐身的敌人会附加指定buff,敌人超出范围时,会移除buff
	public static EDBuff BuffFloatToAir { get { return _BuffFloatToAir ??= mTable.query(BuffFloatToAir_ID); } }// 浮空
	public static EDBuff TriggerTypeTowerOverCountBuffToSelf { get { return _TriggerTypeTowerOverCountBuffToSelf ??= mTable.query(TriggerTypeTowerOverCountBuffToSelf_ID); } }// 场上指定类型的塔超过一定数量时,会给自己附加指定buff,塔数量低于一定数量时会移除buff
	public static EDBuff TriggerBuffToTypeTowerAndSelf { get { return _TriggerBuffToTypeTowerAndSelf ??= mTable.query(TriggerBuffToTypeTowerAndSelf_ID); } }// 给自己和指定类型的塔附加buff
	public static EDBuff TriggerBuffToTypeTower { get { return _TriggerBuffToTypeTower ??= mTable.query(TriggerBuffToTypeTower_ID); } }// 给指定类型的塔附加buff
	public static EDBuff BuffBeenBurnDamageUp { get { return _BuffBeenBurnDamageUp ??= mTable.query(BuffBeenBurnDamageUp_ID); } }// 受到的燃烧伤害提升
	public static EDBuff BuffBeenPoisoningDamageUp { get { return _BuffBeenPoisoningDamageUp ??= mTable.query(BuffBeenPoisoningDamageUp_ID); } }// 受到的中毒伤害提升
	public static EDBuff BuffBeenAllElementDamageDown { get { return _BuffBeenAllElementDamageDown ??= mTable.query(BuffBeenAllElementDamageDown_ID); } }// 受到的所有元素属性伤害降低
	public static EDBuff BuffSneak { get { return _BuffSneak ??= mTable.query(BuffSneak_ID); } }// 潜行
	public static EDBuff BuffVertigoDecrease { get { return _BuffVertigoDecrease ??= mTable.query(BuffVertigoDecrease_ID); } }// 降低被眩晕的概率和眩晕的时间
	public static EDBuff TriggerWillDie { get { return _TriggerWillDie ??= mTable.query(TriggerWillDie_ID); } }// 即将死亡时触发
	public static EDBuff BuffAntiStrickBack { get { return _BuffAntiStrickBack ??= mTable.query(BuffAntiStrickBack_ID); } }// 无法被击退
	public static EDBuff BuffDisarm { get { return _BuffDisarm ??= mTable.query(BuffDisarm_ID); } }// 缴械
	public static EDBuff BuffAntiCriticalUp { get { return _BuffAntiCriticalUp ??= mTable.query(BuffAntiCriticalUp_ID); } }// 暴击抗性提升
	public static EDBuff BuffBeenShockedDamageUp { get { return _BuffBeenShockedDamageUp ??= mTable.query(BuffBeenShockedDamageUp_ID); } }// 受到的感电伤害提升
	public static EDBuff TriggerTypeMonsterDie { get { return _TriggerTypeMonsterDie ??= mTable.query(TriggerTypeMonsterDie_ID); } }// 指定怪物死亡时触发
	public static EDBuff BuffSlowIncrease { get { return _BuffSlowIncrease ??= mTable.query(BuffSlowIncrease_ID); } }// 受到的减速效果提升
	public static EDBuff BuffImmunityElementDebuffDamage { get { return _BuffImmunityElementDebuffDamage ??= mTable.query(BuffImmunityElementDebuffDamage_ID); } }// 免疫所有元素伤害
	public static EDBuff BuffClearDebuff { get { return _BuffClearDebuff ??= mTable.query(BuffClearDebuff_ID); } }// 清除所有debuff
	public static EDBuff TriggerBuffToRangeMonsterOnce { get { return _TriggerBuffToRangeMonsterOnce ??= mTable.query(TriggerBuffToRangeMonsterOnce_ID); } }// 范围内所有怪物附加buff,只在进入状态时添加一次,并且不会主动移除
	public static EDBuff BuffRecoverHPPercentOnce { get { return _BuffRecoverHPPercentOnce ??= mTable.query(BuffRecoverHPPercentOnce_ID); } }// 百分比恢复血量
	public static EDBuff BuffEvasionUp { get { return _BuffEvasionUp ??= mTable.query(BuffEvasionUp_ID); } }// 闪避率提升
	public static EDBuff BuffForceChangeTarget { get { return _BuffForceChangeTarget ??= mTable.query(BuffForceChangeTarget_ID); } }// 强制改变防御塔和英雄的目标选择为指定目标,技能目标为对自己释放的除外
	public static EDBuff BuffShocked { get { return _BuffShocked ??= mTable.query(BuffShocked_ID); } }// 感电
	public static EDBuff BuffFreeze { get { return _BuffFreeze ??= mTable.query(BuffFreeze_ID); } }// 冰冻
	public static EDBuff BuffParalysis { get { return _BuffParalysis ??= mTable.query(BuffParalysis_ID); } }// 麻痹
	public static EDBuff BuffMoveSpeedDownValue { get { return _BuffMoveSpeedDownValue ??= mTable.query(BuffMoveSpeedDownValue_ID); } }// 固定数值减速,减速到负数时,可以让怪物后退
	public static EDBuff BuffPushMove { get { return _BuffPushMove ??= mTable.query(BuffPushMove_ID); } }// 怪物在眩晕等无法移动的状态时，强制移动怪物位置
	public static EDBuff BuffMoveSpeedUpValue { get { return _BuffMoveSpeedUpValue ??= mTable.query(BuffMoveSpeedUpValue_ID); } }// 固定数值加速
	public static EDBuff BuffDamageOverTime { get { return _BuffDamageOverTime ??= mTable.query(BuffDamageOverTime_ID); } }// 无属性持续伤害
	public static EDBuff BuffMoveSpeedDownNoEffect { get { return _BuffMoveSpeedDownNoEffect ??= mTable.query(BuffMoveSpeedDownNoEffect_ID); } }// 百分比减速,固定百分比，无特效
	public static EDBuff BuffInTunnel { get { return _BuffInTunnel ??= mTable.query(BuffInTunnel_ID); } }// 通过隧道时不可攻击状态
	public static EDBuff TriggerBuffToAllTower { get { return _TriggerBuffToAllTower ??= mTable.query(TriggerBuffToAllTower_ID); } }// 给所有防御塔附加buff,持续时间内新增的塔也会被附加buff
	public static EDBuff BuffDamageUp { get { return _BuffDamageUp ??= mTable.query(BuffDamageUp_ID); } }// 伤害增加
	public static EDBuff BuffTowerExploRangeUp { get { return _BuffTowerExploRangeUp ??= mTable.query(BuffTowerExploRangeUp_ID); } }// 增加子弹爆炸范围
	public static EDBuff BuffZhenDangTaBulletCountUp { get { return _BuffZhenDangTaBulletCountUp ??= mTable.query(BuffZhenDangTaBulletCountUp_ID); } }// 增加电磁震荡塔能量球个数
	public static EDBuff BuffResetLastSkillCD { get { return _BuffResetLastSkillCD ??= mTable.query(BuffResetLastSkillCD_ID); } }// 重置刚释放过的技能CD
	public static EDBuff TriggerFireSkill { get { return _TriggerFireSkill ??= mTable.query(TriggerFireSkill_ID); } }// 释放技能时触发
	public static EDBuff TriggerFireCountSkillHit { get { return _TriggerFireCountSkillHit ??= mTable.query(TriggerFireCountSkillHit_ID); } }// 释放一定次数技能后的命中时触发
	public static EDBuff BuffRangeDamageOnce { get { return _BuffRangeDamageOnce ??= mTable.query(BuffRangeDamageOnce_ID); } }// 造成一次范围攻击力百分比伤害
	public static EDBuff TriggerFireCountSkillWillGenerateDamage { get { return _TriggerFireCountSkillWillGenerateDamage ??= mTable.query(TriggerFireCountSkillWillGenerateDamage_ID); } }// 释放一定次数技能后的即将命中时触发
	public static EDBuff BuffAlwaysCriticalHit { get { return _BuffAlwaysCriticalHit ??= mTable.query(BuffAlwaysCriticalHit_ID); } }// 攻击必定暴击
	public static EDBuff BuffTypeTowerIncreaseSelfAttack { get { return _BuffTypeTowerIncreaseSelfAttack ??= mTable.query(BuffTypeTowerIncreaseSelfAttack_ID); } }// 场上指定类型的塔越多,自身增加的攻击力越多
	public static EDBuff BuffTypeTowerIncreaseSelfAttackSpeed { get { return _BuffTypeTowerIncreaseSelfAttackSpeed ??= mTable.query(BuffTypeTowerIncreaseSelfAttackSpeed_ID); } }// 场上指定类型的塔越多,自身增加的攻速越多
	public static EDBuff BuffDamageUpBeforeWave { get { return _BuffDamageUpBeforeWave ??= mTable.query(BuffDamageUpBeforeWave_ID); } }// 前一定波数伤害增加
	public static EDBuff BuffIncreaseBulletCount { get { return _BuffIncreaseBulletCount ??= mTable.query(BuffIncreaseBulletCount_ID); } }// 增加子弹数量
	public static EDBuff BuffAttackSpeedDown { get { return _BuffAttackSpeedDown ??= mTable.query(BuffAttackSpeedDown_ID); } }// 降低攻速
	public static EDBuff BuffSkillRangeDown { get { return _BuffSkillRangeDown ??= mTable.query(BuffSkillRangeDown_ID); } }// 射程降低
	public static EDBuff TriggerKillMonster { get { return _TriggerKillMonster ??= mTable.query(TriggerKillMonster_ID); } }// 击杀怪物时触发
	public static EDBuff TriggerTimeInterval { get { return _TriggerTimeInterval ??= mTable.query(TriggerTimeInterval_ID); } }// 每隔一定时间触发一次
	public static EDBuff BuffBulletFlyDisIncreaseExploRange { get { return _BuffBulletFlyDisIncreaseExploRange ??= mTable.query(BuffBulletFlyDisIncreaseExploRange_ID); } }// 爆炸范围随着飞行距离增加
	public static EDBuff BuffBulletFlyDisIncreaseDamage { get { return _BuffBulletFlyDisIncreaseDamage ??= mTable.query(BuffBulletFlyDisIncreaseDamage_ID); } }// 伤害随着飞行距离增加
	public static EDBuff BuffBulletDamageUpInExploRange { get { return _BuffBulletDamageUpInExploRange ??= mTable.query(BuffBulletDamageUpInExploRange_ID); } }// 对子弹爆炸一定范围内的敌人伤害增加
	public static EDBuff BuffBulletSpeedUp { get { return _BuffBulletSpeedUp ??= mTable.query(BuffBulletSpeedUp_ID); } }// 子弹飞行速度增加
	public static EDBuff TriggerBuffToBulletExploRange { get { return _TriggerBuffToBulletExploRange ??= mTable.query(TriggerBuffToBulletExploRange_ID); } }// 子弹爆炸时给范围内所有敌人附加buff
	public static EDBuff BuffXiangQianTaPurpleGemTargetCountUp { get { return _BuffXiangQianTaPurpleGemTargetCountUp ??= mTable.query(BuffXiangQianTaPurpleGemTargetCountUp_ID); } }// 镶嵌塔紫宝石技能弹射目标数量增加
	public static EDBuff BuffTowerExploRangeDown { get { return _BuffTowerExploRangeDown ??= mTable.query(BuffTowerExploRangeDown_ID); } }// 减少火炮塔爆炸范围
	public static EDBuff BuffIncreaseBulletCountHuoPao { get { return _BuffIncreaseBulletCountHuoPao ??= mTable.query(BuffIncreaseBulletCountHuoPao_ID); } }// 火炮塔子弹数量增加
	public static EDBuff BuffDamageDown { get { return _BuffDamageDown ??= mTable.query(BuffDamageDown_ID); } }// 伤害降低
	public static EDBuff BuffAroundTowerCountAttackUp { get { return _BuffAroundTowerCountAttackUp ??= mTable.query(BuffAroundTowerCountAttackUp_ID); } }// 塔附近1格有n个塔，每个塔增加该塔的攻击
	public static EDBuff BuffAroundTowerCountAttackSpeedUp { get { return _BuffAroundTowerCountAttackSpeedUp ??= mTable.query(BuffAroundTowerCountAttackSpeedUp_ID); } }// 塔附近1格有n个塔，每个塔增加该塔的攻速
	public static EDBuff BuffMoveSpeedDownRouge { get { return _BuffMoveSpeedDownRouge ??= mTable.query(BuffMoveSpeedDownRouge_ID); } }// 肉鸽模式球形飞弹天赋特定减速Buff,百分比
	public static EDBuff TriggerWillHitHpMinPercent { get { return _TriggerWillHitHpMinPercent ??= mTable.query(TriggerWillHitHpMinPercent_ID); } }// 击中前怪物血量高于百分比触发
	public static EDBuff BuffDefenceDownPercent { get { return _BuffDefenceDownPercent ??= mTable.query(BuffDefenceDownPercent_ID); } }// 防御力百分比降低
	public static EDBuff BuffInstantDeath { get { return _BuffInstantDeath ??= mTable.query(BuffInstantDeath_ID); } }// 即死
	public static EDBuff TriggerWillHitHpMaxPercent { get { return _TriggerWillHitHpMaxPercent ??= mTable.query(TriggerWillHitHpMaxPercent_ID); } }// 击中前怪物血量低于百分比触发
	public static EDBuff BuffDamageUpStrengthMonster { get { return _BuffDamageUpStrengthMonster ??= mTable.query(BuffDamageUpStrengthMonster_ID); } }// 攻击指定怪物时攻击力百分比加成
	public static EDBuff BuffAttackSpeedUpStepped { get { return _BuffAttackSpeedUpStepped ??= mTable.query(BuffAttackSpeedUpStepped_ID); } }// 初始攻击间隔提高，每次攻击降低攻击间隔，有叠加上限，若一定时间内未进行攻击则重置
	public static EDBuff TriggerBuffOnWaveStart { get { return _TriggerBuffOnWaveStart ??= mTable.query(TriggerBuffOnWaveStart_ID); } }// 每波开始，概率添加某些状态，没随机到就会移除
	public static EDBuff BuffCriticalDamageUp { get { return _BuffCriticalDamageUp ??= mTable.query(BuffCriticalDamageUp_ID); } }// 暴击伤害增加
	public static EDBuff BuffIncreaseBulletCountByNoDamageTime { get { return _BuffIncreaseBulletCountByNoDamageTime ??= mTable.query(BuffIncreaseBulletCountByNoDamageTime_ID); } }// 一段时间内没有造成伤害，下次攻击子弹增加
	public static EDBuff BuffRangeDamageByHpMaxPercent { get { return _BuffRangeDamageByHpMaxPercent ??= mTable.query(BuffRangeDamageByHpMaxPercent_ID); } }// 击杀怪物时,对周围指定类型的怪物造成当前怪物最大血量百分比的伤害
	public static EDBuff BuffIncreaseBulletCountPercent { get { return _BuffIncreaseBulletCountPercent ??= mTable.query(BuffIncreaseBulletCountPercent_ID); } }// 百分比增加子弹数量
	public static EDBuff TriggerBuffByWaveBulletCount { get { return _TriggerBuffByWaveBulletCount ??= mTable.query(TriggerBuffByWaveBulletCount_ID); } }// 塔释放一定数量子弹,触发buff,直到波次结束
	public static EDBuff BuffRogueKillMonsterAddBuildCoin { get { return _BuffRogueKillMonsterAddBuildCoin ??= mTable.query(BuffRogueKillMonsterAddBuildCoin_ID); } }// 肉鸽模式，塔每击杀n个敌人，获得m肉鸽建造点
	public static EDBuff BuffAttackUpOnceByKillMonster { get { return _BuffAttackUpOnceByKillMonster ??= mTable.query(BuffAttackUpOnceByKillMonster_ID); } }// 击杀n个敌人后，下一次攻击提高
	public static EDBuff BuffRogueKillMonsterCureLevelHp { get { return _BuffRogueKillMonsterCureLevelHp ??= mTable.query(BuffRogueKillMonsterCureLevelHp_ID); } }// 每击杀n个敌人，回复m点已损失的羊村生命
	public static EDBuff BuffAttackDownThenUpByHitSameMonster { get { return _BuffAttackDownThenUpByHitSameMonster ??= mTable.query(BuffAttackDownThenUpByHitSameMonster_ID); } }// 攻击力降低，每次攻击提高攻击力。上限n层，切换目标时重置
	public static EDBuff BuffChangeSearchTargetType { get { return _BuffChangeSearchTargetType ??= mTable.query(BuffChangeSearchTargetType_ID); } }// 寻敌方式修改
	public static EDBuff BuffDamageUpToDebuffMonster { get { return _BuffDamageUpToDebuffMonster ??= mTable.query(BuffDamageUpToDebuffMonster_ID); } }// 对处于异常状态下的单位造成的伤害提升
	public static EDBuff BuffRogueKillMonsterFreeUpLevel { get { return _BuffRogueKillMonsterFreeUpLevel ??= mTable.query(BuffRogueKillMonsterFreeUpLevel_ID); } }// 击杀n个敌人后，该塔升级免费
	public static EDBuff BuffScaleBullet { get { return _BuffScaleBullet ??= mTable.query(BuffScaleBullet_ID); } }// 调整子弹大小
	public static EDBuff BuffBulletAttackUpHitMonster { get { return _BuffBulletAttackUpHitMonster ??= mTable.query(BuffBulletAttackUpHitMonster_ID); } }// 波动塔子弹每穿过一个敌方单位，子弹攻击提高
	public static EDBuff BuffIncreaseFlyDisByRogueTowerLevel { get { return _BuffIncreaseFlyDisByRogueTowerLevel ??= mTable.query(BuffIncreaseFlyDisByRogueTowerLevel_ID); } }// 肉鸽模式，按塔等级增加子弹飞行距离
	public static EDBuff BuffIncreaseBulletBounceTimes { get { return _BuffIncreaseBulletBounceTimes ??= mTable.query(BuffIncreaseBulletBounceTimes_ID); } }// 增加弹跳子弹的弹跳次数
	public static EDBuff BuffKillMonsterChangeRandomTower { get { return _BuffKillMonsterChangeRandomTower ??= mTable.query(BuffKillMonsterChangeRandomTower_ID); } }// 击杀n个敌人后，随机变成一个塔，保留等级
	public static EDBuff BuffIncreaseHuoPaoExplosionMulti { get { return _BuffIncreaseHuoPaoExplosionMulti ??= mTable.query(BuffIncreaseHuoPaoExplosionMulti_ID); } }// 火炮子弹概率爆炸多次
	public static EDBuff BuffZhenDangNotDestroyBulletOnHit { get { return _BuffZhenDangNotDestroyBulletOnHit ??= mTable.query(BuffZhenDangNotDestroyBulletOnHit_ID); } }// 设置电磁震荡塔子弹不消失
	public static EDBuff TriggerBuffByEnterTowerRange { get { return _TriggerBuffByEnterTowerRange ??= mTable.query(TriggerBuffByEnterTowerRange_ID); } }// 向进入该塔射程范围内的怪物添加buff
	public static EDBuff BuffMoveSpeedDownZhenDownTowerRange { get { return _BuffMoveSpeedDownZhenDownTowerRange ??= mTable.query(BuffMoveSpeedDownZhenDownTowerRange_ID); } }// 电磁震荡塔射程减速buff，不叠加
	public static EDBuff BuffZhenDangAddBulletByKillMonster { get { return _BuffZhenDangAddBulletByKillMonster ??= mTable.query(BuffZhenDangAddBulletByKillMonster_ID); } }// 电磁震荡塔击杀n个敌人，获得能量球
	public static EDBuff TriggerBuffWhenBulletConsume { get { return _TriggerBuffWhenBulletConsume ??= mTable.query(TriggerBuffWhenBulletConsume_ID); } }// 消耗n个子弹后触发buff
	public static EDBuff TriggerBuffToHexRangeTower { get { return _TriggerBuffToHexRangeTower ??= mTable.query(TriggerBuffToHexRangeTower_ID); } }// 对六边形半径范围内的塔触发buff
	public static EDBuff BuffZhenDangConsumeCriticalUp { get { return _BuffZhenDangConsumeCriticalUp ??= mTable.query(BuffZhenDangConsumeCriticalUp_ID); } }// 电磁震荡塔子弹消耗的特殊暴击加成buff
	public static EDBuff TriggerBuffGlobalWhenBulletExplosionHuoPao { get { return _TriggerBuffGlobalWhenBulletExplosionHuoPao ??= mTable.query(TriggerBuffGlobalWhenBulletExplosionHuoPao_ID); } }// 当塔的子弹爆炸时，对战斗中的全局角色触发buff
	public static EDBuff TriggerBuffWithAreaCollider { get { return _TriggerBuffWithAreaCollider ??= mTable.query(TriggerBuffWithAreaCollider_ID); } }// 生成一个区域模型，根据他的碰撞箱，对其中的怪物附加buff
	public static EDBuff BuffMoveSpeedDownHuoPaoExplosionArea { get { return _BuffMoveSpeedDownHuoPaoExplosionArea ??= mTable.query(BuffMoveSpeedDownHuoPaoExplosionArea_ID); } }// 火炮塔子弹爆炸残留提供的减速buff
	public static EDBuff BuffFocusAttackMonster { get { return _BuffFocusAttackMonster ??= mTable.query(BuffFocusAttackMonster_ID); } }// 集火怪物的buff
	public static EDBuff TriggerBuffToGridRangeTowerWhenPlace { get { return _TriggerBuffToGridRangeTowerWhenPlace ??= mTable.query(TriggerBuffToGridRangeTowerWhenPlace_ID); } }// 在放置和移动英雄时,对一定范围内的塔触发buff
	public static EDBuff BuffWaveUpTowerNear { get { return _BuffWaveUpTowerNear ??= mTable.query(BuffWaveUpTowerNear_ID); } }// 每过n回合，对旁边的防御塔升级
	public static EDBuff BuffAttackSpeedUpGongJianShou { get { return _BuffAttackSpeedUpGongJianShou ??= mTable.query(BuffAttackSpeedUpGongJianShou_ID); } }// 弓手特殊攻速增加
	public static EDBuff BuffRogueHitMonsterAddBuildCoin { get { return _BuffRogueHitMonsterAddBuildCoin ??= mTable.query(BuffRogueHitMonsterAddBuildCoin_ID); } }// 每击中n个敌人，获得m银币
	public static EDBuff BuffRogueCoinInterest { get { return _BuffRogueCoinInterest ??= mTable.query(BuffRogueCoinInterest_ID); } }// 肉鸽回合结束时，每有n银币，额外获得m银币，m有上限
	public static EDBuff BuffRogueMonsterBreakAddCoin { get { return _BuffRogueMonsterBreakAddCoin ??= mTable.query(BuffRogueMonsterBreakAddCoin_ID); } }// 肉鸽模式前n个进入基地的怪物转化为m银币，boss无效
	public static EDBuff BuffImmunityPhysicDamage { get { return _BuffImmunityPhysicDamage ??= mTable.query(BuffImmunityPhysicDamage_ID); } }// 免疫物理伤害
	public static EDBuff BuffAttackSpeedUpByKillMonster { get { return _BuffAttackSpeedUpByKillMonster ??= mTable.query(BuffAttackSpeedUpByKillMonster_ID); } }// 每击杀n单位，攻速增加
	public static EDBuff BuffAttackCriticalUpByTowerCount { get { return _BuffAttackCriticalUpByTowerCount ??= mTable.query(BuffAttackCriticalUpByTowerCount_ID); } }// 根据场上防御塔数量，提升自身攻击力和暴击率
	public static EDBuff TriggerBuffToGridRangeTowerByTowerCount { get { return _TriggerBuffToGridRangeTowerByTowerCount ??= mTable.query(TriggerBuffToGridRangeTowerByTowerCount_ID); } }// 在放置和移动英雄时，且有一定数量的某种塔时，对一定范围内的塔触发buff
	public static EDBuff BuffSpeedAttackUpByTowerCount { get { return _BuffSpeedAttackUpByTowerCount ??= mTable.query(BuffSpeedAttackUpByTowerCount_ID); } }// 根据场上某种防御塔数量，提升自身攻速和攻击
	public static EDBuff BuffAttackUpByGridTowerCount { get { return _BuffAttackUpByGridTowerCount ??= mTable.query(BuffAttackUpByGridTowerCount_ID); } }// 附近n格内有m个塔时，提升攻击力
	public static EDBuff BuffAddTowerRogue { get { return _BuffAddTowerRogue ??= mTable.query(BuffAddTowerRogue_ID); } }// 将防御塔带入肉鸽战斗

	public string mName;							// buff名字
	public bool mIsTrigger;							// 是否为触发器类型
	public int mDebuffGroupID;						// debuff组的ID
	public bool mDeadCanTrigger;					// 死亡时是否也可以触发
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.read(out mIsTrigger);
		result = result && reader.read(out mDebuffGroupID);
		result = result && reader.read(out mDeadCanTrigger);
		return result;
	}
}
// auto generate end