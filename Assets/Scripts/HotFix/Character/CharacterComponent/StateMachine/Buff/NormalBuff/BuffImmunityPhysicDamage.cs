using static FrameBaseHotFix;

// 参数
public class BuffImmunityPhysicDamageParam : CharacterBuffParamT<BuffImmunityPhysicDamageParam>
{
	public override void registeAllParam(){}
	public override void check() { }
}

// 免疫物理伤害（免伤族代表；另有 BuffImmunityElementDebuffDamage 管元素异常伤害）。
// 范式：enter/leave 对 GameData 的计数字段 ++/--，用计数而非 bool，支持多个同类 Buff 叠加。
// 构造函数设 STATE_MUTEX.NO_NEW——同名免疫已存在时不再添加新的，避免重复。
// 注：原类头注释写"免疫所有元素伤害"，与类名/字段 mImmunityPhysicDamage(物理)不符，疑似笔误，仅标注。
public class BuffImmunityPhysicDamage : CharacterBuffT<BuffImmunityPhysicDamageParam>
{
	public BuffImmunityPhysicDamage()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void enter()
	{
		base.enter();
		++mCharacterGame.getGameData().mImmunityPhysicDamage;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		--mCharacterGame.getGameData().mImmunityPhysicDamage;
	}
}