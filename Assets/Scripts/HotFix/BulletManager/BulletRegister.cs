using System;
using System.Collections.Generic;
using static UnityUtility;
using static GBR;

// 子弹类型注册
public class BulletRegister
{
	protected static ParamParseCollection mParamCollection = new();
	protected static Dictionary<BULLET_TYPE, Type> mBulletTypeList = new();            // 子弹类型注册表
	public static void registerAll()
	{
		registeBullet<SkillBulletTrack, BulletCustomParam_Track>(BULLET_TYPE.TRACK);
		registeBullet<SkillBulletParabolaTrack, BulletCustomParam_ParabolaTrack>(BULLET_TYPE.PARABOLA_TRACK);
		registeBullet<SkillBulletLinkLine, BulletCustomParam_LinkLine>(BULLET_TYPE.LINK_LINE);
		registeBullet<SkillBulletNoMove, BulletCustomParam_NoMove>(BULLET_TYPE.NO_MOVE);
		registeBullet<SkillBulletStraightLine, BulletCustomParam_StraightLine>(BULLET_TYPE.STRAIGHT_LINE);
		registeBullet<SkillBulletStraightLineAlwaysCollide, BulletCustomParam_StraightLineAlwaysCollide>(BULLET_TYPE.STRAIGHT_LINE_ALWAYS_COLLIDE);
		registeBullet<SkillBulletCurveMultiDamage, BulletCustomParam_CurveMultiDamage>(BULLET_TYPE.CURVE_MULTI_DAMAGE);
		registeBullet<SkillBulletNoMoveFan, BulletCustomParam_NoMoveFan>(BULLET_TYPE.NO_MOVE_FAN);
		registeBullet<SkillBulletCurve, BulletCustomParam_Curve>(BULLET_TYPE.CURVE);
		registeBullet<SkillBulletParabola, BulletCustomParam_Parabola>(BULLET_TYPE.PARABOLA);
		registeBullet<SkillBulletBalloon, BulletCustomParam_Balloon>(BULLET_TYPE.BALLOON);
		registeBullet<SkillBulletRotateAround, BulletCustomParam_RotateAround>(BULLET_TYPE.ROTATE_AROUND);
		registeBullet<SkillBulletZhenDang, BulletCustomParam_ZhenDang>(BULLET_TYPE.ZHEN_DANG);
		registeBullet<SkillBulletGouZhua, BulletCustomParam_GouZhua>(BULLET_TYPE.GOU_ZHUA);
		registeBullet<SkillBulletTrackBounce, BulletCustomParam_TrackBounce>(BULLET_TYPE.TRACK_BOUNCE);
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