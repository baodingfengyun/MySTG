using static StringUtility;

// 参数
public class BuffBleedingParam : CharacterBuffParamT<BuffBleedingParam>
{
	public float mBleedingPercent;         // 出血百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mBleedingPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBleedingParam other)
	{
		mBleedingPercent = other.mBleedingPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mBleedingPercent = 0.0f;
	}
}

// 每个流血都独立计算,只是在产生伤害时会判断当前流血是否为角色身上伤害最高的流血,如果是,则生效,如果不是,则不会产生伤害
// 所以为了提高效率,不会在此buff中进行伤害判断,只是通知角色有流血,具体逻辑由角色自己处理
public class BuffBleeding : CharacterBuffT<BuffBleedingParam>
{
	public float mBleedingPercent;
	public override void resetProperty()
	{
		base.resetProperty();
		mBleedingPercent = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mBleedingPercent = mCustomParam.mBleedingPercent;
		mCharacterGame.getOrAddComponent<COMCharacterBleeding>()?.addBleeding(mBleedingPercent);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getOrAddComponent<COMCharacterBleeding>()?.removeBleeding(mBleedingPercent);
	}
}