
// 行为动作状态
public class StateGroupAction : StateGroup { }

// 不允许移动的状态
public class StateGroupNotAllowMove : StateGroup { }

// 不允许攻击的状态
public class StateGroupNotAllowAttack : StateGroup { }

// debuff,一般是一些不可见的,怪物特性类的,比如怕火,怕电
public class StateGroupDebuff1 : StateGroup { }

// debuff,一般是可见的,明显的效果,比如燃烧,中毒等
public class StateGroupDebuff2 : StateGroup { }

// 燃烧和冰霜减速,冰冻的互斥
public class StateGroupBurnSlowDownFreeze : StateGroup { }

// 中毒和感电的互斥
public class StateGroupPoisonShocked : StateGroup { }