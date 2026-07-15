using static GBR;
using static FrameBaseHotFix;

// 参数
public class TriggerBuffGlobalWhenBulletExplosionHuoPaoParam : CharacterTriggerParamT<TriggerBuffGlobalWhenBulletExplosionHuoPaoParam>
{}

// 当火炮塔的子弹爆炸时，对战斗中的全局角色触发buff
public class TriggerBuffGlobalWhenBulletExplosionHuoPao : CharacterTriggerT<TriggerBuffGlobalWhenBulletExplosionHuoPaoParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventBulletExplosionHuoPao>(mCharacter.getGUID(), onBulletExplosionHuoPao, this);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletExplosionHuoPao(EventBulletExplosionHuoPao param)
	{
		addBuff(mTowerDefenceSystem.getGlobalCharacter(), null, param.mBullet);
	}
}