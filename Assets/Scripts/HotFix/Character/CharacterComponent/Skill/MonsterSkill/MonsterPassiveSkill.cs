using static MathUtility;
using static FrameBaseHotFix;
using static GBR;

// 怪物被动技能
public class MonsterPassiveSkill : MonsterSkillBase
{
	public virtual void fire()
	{
		mMonster.setMP(clampMin(mMonster.getMP() - mSkillData.mMP));

		// 给目标附加buff
		foreach (int buffDetailID in mSkillData.mDefaultFireBuff)
		{
			using var a = new BuffParamScope(out CharacterBuffParam param, buffDetailID);
			if (mMonster.getStateMachine().addState(mStateManager.getStateType(param.mBuffData.mID), param, 0) is not CharacterTrigger trigger)
			{
				continue;
			}
			trigger.setWillAddBuffCallback((_, _, buffParam) =>
			{
				buffParam.mSource = mMonster;
			});
			trigger.setCustomTriggerCallback(getCustomTriggerCallback());
			// mDefaultFireBuff中可配置多个buff,但是mPassiveTriggerEffect只配了一个,所以认为是任意一个触发类的buff触发时就播放此特效
			int triggerEffect = mSkillData.mPassiveTriggerEffect;
			if (triggerEffect > 0)
			{
				trigger.setWillTriggerCallback((character, trigger) =>
				{
					onTrigger(trigger);
					// 释放触发的特效
					EDEffect effectData = mExcelEffect.query(triggerEffect);
					mEffectManager.playEffectAsyncAtPosition(effectData.mPath, character.getPosition(), 1.0f, effectData.mSupportMoveToHide, 0);
					AT.SOUND_2D(mSkillData.mPassiveTriggerSFX);
				});
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void onTrigger(CharacterTrigger trigger) { }
	protected virtual BuffTriggerCallback getCustomTriggerCallback() { return null; }
}