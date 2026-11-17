
// ===== 状态互斥组(StateGroup)：给状态分类，实现"同类状态互斥" =====
// 【作用】每个 Buff/动作状态声明自己属于哪个 Group；同一角色身上同Group的状态不能共存，
//   加新的会自动顶掉旧的(具体顶替策略由状态的 mMutexType 决定)。
// 【用途举例】
//   StateGroupAction      —— 走路/站立/施法动画等动作状态互斥，保证同一时刻只播一个动作；
//   StateGroupNotAllowMove / NotAllowAttack —— 眩晕/冰冻挂上时，移动/攻击组件查这个组来禁用行为；
//   StateGroupDebuff1     —— 不可见的特性 debuff(怕火/怕电)，通常给怪物用的被动标记；
//   StateGroupDebuff2     —— 可见的 debuff(燃烧/中毒)。
// 【互斥特例】下面两组把"本应能共存但设计上要互斥"的状态归到一组：
//   燃烧/冰霜减速/冰冻互斥、中毒/感电互斥——避免同类元素效果无限叠加。
public class StateGroupAction : StateGroup { }

// 不允许移动的状态（眩晕/冰冻等挂上时，移动组件据此禁用）
public class StateGroupNotAllowMove : StateGroup { }

// 不允许攻击的状态（缴械等挂上时，攻击组件据此禁用）
public class StateGroupNotAllowAttack : StateGroup { }

// debuff,一般是一些不可见的,怪物特性类的,比如怕火,怕电
public class StateGroupDebuff1 : StateGroup { }

// debuff,一般是可见的,明显的效果,比如燃烧,中毒等
public class StateGroupDebuff2 : StateGroup { }

// 燃烧和冰霜减速,冰冻的互斥
public class StateGroupBurnSlowDownFreeze : StateGroup { }

// 中毒和感电的互斥
public class StateGroupPoisonShocked : StateGroup { }