
// 怪物血量改变
public class EventMonsterHPChange : GameEvent
{
	public CharacterMonster mMonster;
	public int mCurHP;
	public int mLastHP;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
		mCurHP = 0;
		mLastHP = 0;
	}
}