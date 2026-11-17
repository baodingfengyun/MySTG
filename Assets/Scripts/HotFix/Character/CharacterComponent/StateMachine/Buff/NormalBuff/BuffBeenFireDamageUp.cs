using static StringUtility;

// 参数
public class BuffBeenFireDamageUpParam : CharacterBuffParamT<BuffBeenFireDamageUpParam>
{
	public float mIncrease;     // 提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenFireDamageUpParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 受到的火属性伤害提升（Been 元素克制族的代表）。
// 全族约 25 个类（BuffBeenXxxDamageUp/Down、BuffBeenAllElementDamageDown、BuffBeenXxxingDamageUp…），
// 实现完全同构：enter 把增量加到 GameData 的某个 mBeen*ElementDamageIncrease 字段，leave 减回去，严格配对。
// 这些字段由伤害管线 generateDamage 读取——所以"易伤/抗性"类词条不需要改伤害公式，只填表改字段即可。
// 本类对应字段 mBeenFireElementDamageIncrease；同类还有冰/雷/暗/光/毒/燃烧/感电等，逻辑一模一样。
public class BuffBeenFireDamageUp : CharacterBuffT<BuffBeenFireDamageUpParam>
{
	protected float mIncrease;      // 提升的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		// mCustomParam 只在 enter 期间有效（对象池复用），必须把值拷到成员 mIncrease，
		// leave 时才能用同一个值精确减回去——这是所有"数值加减族"Buff 的通用写法。
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mBeenFireElementDamageIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 必须与 enter 严格对称：加多少减多少，否则 Buff 消失后易伤会残留。
		mCharacterGame.getGameData().mBeenFireElementDamageIncrease -= mIncrease;
	}
}