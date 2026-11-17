using static StringUtility;

// 参数
public class BuffBeenIceDamageDownParam : CharacterBuffParamT<BuffBeenIceDamageDownParam>
{
	public float mDecrease;     // 降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenIceDamageDownParam other)
	{
		mDecrease = other.mDecrease;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
}

// 受到的冰属性伤害降低（Been 元素克制族的 Down 方向代表）。
// 与 BuffBeenFireDamageUp 上下对称，且共用同一个 GameData 字段：Up 是 +=，本类 Down 是 -=，
// leave 时反向还原。伤害管线 generateDamage 统一读 mBeen*ElementDamageIncrease，正值易伤、负值抗性。
// 读本类即可理解整个 Up/Down 元素族：名字里的元素(火/冰/雷/暗/光/毒/燃烧/感电)只决定字段名，逻辑一致。
public class BuffBeenIceDamageDown : CharacterBuffT<BuffBeenIceDamageDownParam>
{
	protected float mDecrease;      // 提升的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mDecrease = mCustomParam.mDecrease;
		mCharacterGame.getGameData().mBeenIceElementDamageIncrease -= mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBeenIceElementDamageIncrease += mDecrease;
	}
}