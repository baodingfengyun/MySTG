using static FrameBaseHotFix;

// ===== Buff 体系基类：所有 Buff / 触发器的根 =====
// 【继承链】CharacterState(框架状态机状态) → CharacterBuff(本类) → 两条支线：
//   ① CharacterBuffT<T> → 各 NormalBuff(普通效果：毒/燃烧/减速/加攻...)；
//   ② CharacterTrigger  → 各 Trigger(触发器：命中/击杀/血量...，会自己再生出别的 Buff)。
// 【设计】Buff 本质是"挂到角色状态机上的一个 CharacterState"， enter=生效, leave=失效。
//   所以 Buff 的持续时间、叠加、互斥都复用状态机能力(StateGroup/互斥类型)，不另造轮子。
// 【生命周期事件】Buff 加上/移除时分别向事件系统推 EventMonster/TowerAddBuff、RemoveBuff，
//   供"身上有 N 个 buff 时触发某效果"这类被动监听。
// 【来源】mBuffDetailData 是 Buff 细分表的一行(持续时间/数值都从表读)，子类据此表现。
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
		// 先回调"添加成功"：由触发器挂上的 Buff 会借此让触发器重置 CD
		//   （触发器用 mTriggerAssignID 校验是不是自己挂的，防止误重置）
		thisParam?.mCallback?.Invoke(true, thisParam.mBuffTrigger, thisParam.mTriggerAssignID);
		if (mCharacterGame is CharacterMonster monster)		// 怪物
		{
			using var a = new ClassScope<EventMonsterAddBuff>(out var param);
			param.mMonster = monster;
			mEventSystem.pushEvent(param, mCharacterGame.getGUID());
		}
		else if (mCharacterGame is CharacterTower tower)	// 塔
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