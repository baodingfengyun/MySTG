using static FrameBaseHotFix;
using static GBR;

// 怪物被动技能：开战(notifyFightStart)时触发一次，给"自己"挂上触发器，此后由触发器按条件生效
// 【与主动技能的区别】被动不发子弹、不等血量阈值，而是把自己的 DefaultFireBuff 作为触发器
//   挂到怪物自身状态机上，之后"命中/击杀/死亡"等事件由触发器决定是否再生 Buff。
// 【来源标注】setWillAddBuffCallback 把 mSource 设为怪物自己，便于伤害归因。
// 【触发特效】被动可配一个 mPassiveTriggerEffect，任意触发器触发时播特效+音效（多个触发器共用一个特效）。
// 【扩展钩子】onTrigger / getCustomTriggerCallback 供子类定制，自爆技能就是重写 onTrigger 实现的。
public class MonsterPassiveSkill : MonsterSkillBase
{
	public virtual void fire()
	{
		mMonster.setMP((mMonster.getMP() - mSkillData.mMP).clampMin());

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