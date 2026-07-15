using System.Collections.Generic;
using UnityEngine;
using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 怪物被动技能,自爆,由于触发时间点是怪物销毁以后,比较特殊,所以单独写逻辑
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