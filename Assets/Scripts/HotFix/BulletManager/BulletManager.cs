using System.Collections.Generic;
using static FrameUtility;
using static FrameBaseHotFix;

// 用来管理所有的子弹,tick所有的子弹（全链路 ④中子弹的"容器 + 生命期管理员"）
// createBullet() 是工厂入口：按子弹表里的 BULLET_TYPE 从 BulletRegister 反射出对应子类
// （直线/抛物/追踪/弹射/回旋/扇形…共 16 种形态），这就是"换子弹形态只改配置"的底层支撑。
// 每帧 update 遍历 tick 所有子弹，lateUpdate 处理朝向；销毁走 mDeadBullet 缓冲，
// 延迟到遍历结束后统一清理，避免在遍历中修改字典。destroyAllBullet 在退出战斗时兜底清场。
public class BulletManager : FrameSystem
{
	protected Dictionary<long, SkillBullet> mBulletList = new();					// 所有子弹实例列表
	protected HashSet<SkillBullet> mDeadBullet = new();								// 待销毁的子弹列表
	// 创建子弹实例
	public SkillBullet createBullet(EDSkillBullet bulletData)
	{
		// 子弹类型
		BULLET_TYPE bulletType = bulletData.mType;
		// 创建子弹对象
		var bullet = CLASS(BulletRegister.getBulletType(bulletType)) as SkillBullet;
		bullet.init();
		bullet.initData(bulletData, BulletRegister.getParamTemplate(bulletData));
		return mBulletList.add(bullet.getObjectID(), bullet);
	}
	// 子弹更新
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		// 遍历所有子弹
		foreach (SkillBullet bullet in mBulletList.Values)
		{
			bullet.update(elapsedTime);
		}
		if (mDeadBullet.Count > 0)
		{
			// 销毁子弹
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