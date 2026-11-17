using System.Collections.Generic;
using UnityEngine;
using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 自爆技能：怪物死后的特殊被动——死亡触发器触发后，延迟给周围塔/英雄挂缴械等 Debuff
// 【为什么单独写】普通被动的触发特效在触发瞬间播；自爆的时机是"怪物销毁以后"，
//   需要延迟(mPassiveTriggerDelay)执行，且目标是周围单位而非自己，所以重写 onTrigger。
// 【当前状态】delayCall 里的回调是空的(()=>{})——真实爆炸效果似乎尚未填充/被裁掉，
//   addBuff 工具方法是现成的(按 detailID 查 buffTypeID → 反射加状态)。这是理解时的注意点。
public class MonsterPassiveSkill_SelfExplosion : MonsterPassiveSkill
{
	protected override void onTrigger(CharacterTrigger trigger)
	{
		base.onTrigger(trigger);
		List<int> detailIDList = trigger.getBuffDetailIDList();
		if (detailIDList.Count == 0)
		{
			return;
		}
		int detailID = detailIDList[0];
		Vector3 pos = mMonster.getPosition();
		// 延迟给周围防御塔和英雄添加缴械buff
		delayCall(mSkillData.mPassiveTriggerDelay , () =>{}, this);
	}
	protected static void addBuff(CharacterGame character, int detailID)
	{
		using var a = new BuffParamScope(out CharacterBuffParam param, detailID);
		int buffTypeID = mExcelBuffDetail.query(detailID).mBuffTypeID;
		character.getStateMachine().addState(mStateManager.getStateType(buffTypeID), param, 0);
	}
}