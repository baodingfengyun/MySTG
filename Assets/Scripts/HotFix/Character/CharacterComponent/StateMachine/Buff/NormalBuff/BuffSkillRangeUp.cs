using static GBR;
using static StringUtility;

// 参数
public class BuffSkillRangeUpParam : CharacterBuffParamT<BuffSkillRangeUpParam>
{
	public float mIncreasePercent;         // 射程增加的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffSkillRangeUpParam other)
	{
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}

// 射程增加
public class BuffSkillRangeUp : CharacterBuffT<BuffSkillRangeUpParam>
{
	protected float mRange;		// 射程增加的绝对值
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mRange = mCharacterGame.getOriginRange() * mCustomParam.mIncreasePercent;
		mCharacterGame.setIncreaseRange(mCharacterGame.getIncreaseRange() + mRange);
		if (mCharacterGame is CharacterTower)
		{
			// 如果当前正在显示范围,则需要刷新范围
			mBattleScene.showTowerRange(mTowerDefenceSystem.getSelectedTowerScene());
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.setIncreaseRange(mCharacterGame.getIncreaseRange() - mRange);
		if (mCharacterGame is CharacterTower)
		{
			mBattleScene.showTowerRange(mTowerDefenceSystem.getSelectedTowerScene());
		}
	}
}