
// 参数
public class BuffConfusionParam : CharacterBuffParamT<BuffConfusionParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 混乱（控制族代表：Buff 不改数值，而是直接操作组件状态）。
// enter 调移动组件的 setConfusion(true) 让怪物倒着走，leave 复位 false。
// 这是"Buff 作为行为开关"的一类——同族还有 BuffHoldPosition(禁锢)、BuffDisableSkill(禁技能)、
// BuffFlyable(变飞行)、BuffForceChangeTarget(改索敌)等，都是 enter 开、leave 关某个组件/状态。
// 对比数值族：这类不需要在 resetProperty 里存值，因为效果完全由组件自身状态承载。
public class BuffConfusion : CharacterBuffT<BuffConfusionParam>
{
	public override void enter()
	{
		base.enter();
		mCharacterGame.getComponent<COMMonsterMovement>()?.setConfusion(true);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getComponent<COMMonsterMovement>()?.setConfusion(false);
	}
}