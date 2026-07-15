using static FrameBaseHotFix;

// 角色的buff
public class CharacterBuff : CharacterState
{
	protected EDBuffDetail mBuffDetailData;		// 表格数据
	protected CharacterGame mCharacterGame;		// 所属角色
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffDetailData = null;
		mCharacterGame = null;
	}
	public override void enter()
	{
		base.enter();
		var thisParam = getParam() as CharacterBuffParam;
		thisParam?.mCallback?.Invoke(true, thisParam.mBuffTrigger, thisParam.mTriggerAssignID);
		if (mCharacterGame is CharacterMonster monster)
		{
			using var a = new ClassScope<EventMonsterAddBuff>(out var param);
			param.mMonster = monster;
			mEventSystem.pushEvent(param, mCharacterGame.getGUID());
		}
		else if (mCharacterGame is CharacterTower tower)
		{
			using var a = new ClassScope<EventTowerAddBuff>(out var param);
			param.mTower = tower;
			mEventSystem.pushEvent(param, mCharacterGame.getGUID());
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame is CharacterMonster monster)
		{
			using var a = new ClassScope<EventMonsterRemoveBuff>(out var eventParam);
			eventParam.mMonster = monster;
			mEventSystem.pushEvent(eventParam, mCharacterGame.getGUID());
		}
		else if (mCharacterGame is CharacterTower tower)
		{
			using var a = new ClassScope<EventTowerRemoveBuff>(out var eventParam);
			eventParam.mTower = tower;
			mEventSystem.pushEvent(eventParam, mCharacterGame.getGUID());
		}
	}
	public override void setCharacter(Character character) 
	{
		base.setCharacter(character);
		mCharacterGame = character as CharacterGame;
	}
	public EDBuffDetail getBuffDetail() { return mBuffDetailData; }
}