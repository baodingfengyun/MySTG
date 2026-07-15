using static GBR;
using static StringUtility;

// 参数
public class BuffSkillRangeDownParam : CharacterBuffParamT<BuffSkillRangeDownParam>
{
	public float mDecreasePercent;         // 射程降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffSkillRangeDownParam other)
	{
		mDecreasePercent = other.mDecreasePercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDecreasePercent = 0.0f;
	}
}

// 射程增加
public class BuffSkillRangeDown : CharacterBuffT<BuffSkillRangeDownParam>
{
	protected float mRange;		// 射程降低的绝对值
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mRange = mCharacterGame.getOriginRange() * mCustomParam.mDecreasePercent;
		mCharacterGame.setIncreaseRange(mCharacterGame.getIncreaseRange() - mRange);
		if (mCharacterGame is CharacterTower)
		{
			// 如果当前正在显示范围,则需要刷新范围
			mBattleScene.showTowerRange(mTowerDefenceSystem.getSelectedTowerScene());
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.setIncreaseRange(mCharacterGame.getIncreaseRange() + mRange);
		if (mCharacterGame is CharacterTower)
		{
			mBattleScene.showTowerRange(mTowerDefenceSystem.getSelectedTowerScene());
		}
	}
}