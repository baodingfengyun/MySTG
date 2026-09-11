using System;
using System.Collections.Generic;
using static UnityUtility;
using static GBR;

// 子弹类型注册
public class BulletRegister
{
	protected static ParamParseCollection mParamCollection = new();
	protected static Dictionary<BULLET_TYPE, Type> mBulletTypeList = new();            // 子弹类型注册表
	// 注册子弹类型、参数与技能
	public static void registerAll()
	{
        // 1  追踪子弹
        registeBullet<SkillBulletTrack, BulletCustomParam_Track>(BULLET_TYPE.TRACK);
        // 2  抛物线,带追踪
        registeBullet<SkillBulletParabolaTrack, BulletCustomParam_ParabolaTrack>(BULLET_TYPE.PARABOLA_TRACK);
        // 3  连线,可以瞬间串联多个目标
        registeBullet<SkillBulletLinkLine, BulletCustomParam_LinkLine>(BULLET_TYPE.LINK_LINE);
        // 4  原地范围伤害子弹
        registeBullet<SkillBulletNoMove, BulletCustomParam_NoMove>(BULLET_TYPE.NO_MOVE);
        // 5  直线飞行的子弹
        registeBullet<SkillBulletStraightLine, BulletCustomParam_StraightLine>(BULLET_TYPE.STRAIGHT_LINE);
        // 6  直线飞行的子弹,飞行过程中会一直检测是否碰到物体
        registeBullet<SkillBulletStraightLineAlwaysCollide, BulletCustomParam_StraightLineAlwaysCollide>(BULLET_TYPE.STRAIGHT_LINE_ALWAYS_COLLIDE);
        // 7  按折线移动,每隔一定时间产生一次
        registeBullet<SkillBulletCurveMultiDamage, BulletCustomParam_CurveMultiDamage>(BULLET_TYPE.CURVE_MULTI_DAMAGE);
        // 8  扇形子弹
        registeBullet<SkillBulletNoMoveFan, BulletCustomParam_NoMoveFan>(BULLET_TYPE.NO_MOVE_FAN);
        // 9  按折线移动
        registeBullet<SkillBulletCurve, BulletCustomParam_Curve>(BULLET_TYPE.CURVE);
        // 10 抛物线,不带追踪
        registeBullet<SkillBulletParabola, BulletCustomParam_Parabola>(BULLET_TYPE.PARABOLA);
        // 11 气球子弹
        registeBullet<SkillBulletBalloon, BulletCustomParam_Balloon>(BULLET_TYPE.BALLOON);
        // 12 绕某个点旋转的子弹
        registeBullet<SkillBulletRotateAround, BulletCustomParam_RotateAround>(BULLET_TYPE.ROTATE_AROUND);
        // 13 震荡塔子弹
        registeBullet<SkillBulletZhenDang, BulletCustomParam_ZhenDang>(BULLET_TYPE.ZHEN_DANG);
        // 14 钩爪
        registeBullet<SkillBulletGouZhua, BulletCustomParam_GouZhua>(BULLET_TYPE.GOU_ZHUA);
        // 15 追踪并且弹射周围目标
        registeBullet<SkillBulletTrackBounce, BulletCustomParam_TrackBounce>(BULLET_TYPE.TRACK_BOUNCE);
        // 16 回旋镖
        registeBullet<SkillBulletBoomerang, BulletCustomParam_Boomerang>(BULLET_TYPE.BOOMERANG);

		foreach (EDSkillBullet item in mExcelSkillBullet.queryAll())
		{
            mParamCollection.registeParamTemplate(item.mID, (int)item.mType, item.mParam0, item.mParam1, item.mParam2, item.mParam3);
        }
	}
	public static Type getBulletType(BULLET_TYPE type)
	{
		if (!mBulletTypeList.TryGetValue(type, out Type classType))
		{
			logError("子弹类型未注册:" + type);
		}
		return classType;
	}
	public static ParamCopyable getParamTemplate(EDSkillBullet data)
	{
		return mParamCollection.getParamTemplate(data.mID) as ParamCopyable;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeBullet<Bullet, Param>(BULLET_TYPE type) where Bullet: SkillBullet where Param: ParamBase
    {
		mBulletTypeList.Add(type, typeof(Bullet));
		mParamCollection.registe<Param>((int)type);
	}
}