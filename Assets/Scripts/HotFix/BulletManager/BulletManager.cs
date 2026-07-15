using System.Collections.Generic;
using static FrameUtility;
using static FrameBaseHotFix;

// 用来管理所有的子弹,tick所有的子弹
public class BulletManager : FrameSystem
{
	protected Dictionary<long, SkillBullet> mBulletList = new();					// 所有子弹实例列表
	protected HashSet<SkillBullet> mDeadBullet = new();								// 待销毁的子弹列表
	public SkillBullet createBullet(EDSkillBullet bulletData)
	{
		BULLET_TYPE bulletType = bulletData.mType;
		// 创建子弹对象
		var bullet = CLASS(BulletRegister.getBulletType(bulletType)) as SkillBullet;
		bullet.init();
		bullet.initData(bulletData, BulletRegister.getParamTemplate(bulletData));
		return mBulletList.add(bullet.getObjectID(), bullet);
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		foreach (SkillBullet bullet in mBulletList.Values)
		{
			bullet.update(elapsedTime);
		}
		if (mDeadBullet.Count > 0)
		{
			foreach (SkillBullet bullet in mDeadBullet)
			{
				bullet.destroy();
				mBulletList.Remove(bullet.getObjectID());
			}
			UN_CLASS_LIST(mDeadBullet);
		}
	}
	public override void lateUpdate(float elapsedTime)
	{
		base.lateUpdate(elapsedTime);
		foreach (SkillBullet bullet in mBulletList.Values)
		{
			bullet.lateUpdate(elapsedTime);
		}
	}
	public void destroyBullet(SkillBullet bullet)
	{
		if (bullet.isWillDestroy() || !mBulletList.ContainsKey(bullet.getObjectID()))
		{
			return;
		}
		bullet.setWillDestroy(true);
		mDeadBullet.Add(bullet);
	}
	public void destroyBullet(SkillBullet bullet, long characterGUID)
	{
		destroyBullet(bullet);
		using var a = new ClassScope<EventBulletConsume>(out var eventParam);
		eventParam.mBullet = bullet;
		mEventSystem.pushEvent(eventParam, characterGUID);
	}
	public void destroyAllBullet()
	{
		foreach (SkillBullet bullet in mBulletList.Values)
		{
			bullet.destroyFireEffectReally();
			bullet.destroy();
		}
		UN_CLASS_LIST(mBulletList);
		mDeadBullet.Clear();
	}
}