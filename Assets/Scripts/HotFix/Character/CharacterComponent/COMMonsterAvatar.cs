using UnityEngine;
using static GDR;
using static GBR;
using static UnityUtility;
using static MathUtility;
using static FrameBaseHotFix;
using static FrameBaseUtility;

// 显示怪物模型,大部分逻辑由基类提供
public class COMMonsterAvatar : COMCharacterAvatar
{
	protected CharacterMonster mMonster;	// 怪物
	protected Collider mCollider;			// 碰撞体
	protected MonsterHPBar mHPBar;			// 血条对象
	protected Transform mFootPoint;			// 脚部击中点
	protected Transform mBodyPoint;			// 身体击中点
	protected Transform mHeadPoint;			// 头部击中点
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mMonster = mComponentOwner as CharacterMonster;
		mRelationship = AVATAR_RELATIONSHIP.AVATAR_ALONE;
	}
	public override void destroy()
	{
		base.destroy();
		mUIHPBar?.destroyHPBar(mHPBar);
		mHPBar = null;
	}
	public override void destroyModel()
	{
		mGlobalTouchSystem?.unregisteCollider(mMonster);
		base.destroyModel();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
		mCollider = null;
		mHPBar = null;
		mFootPoint = null;
		mBodyPoint = null;
		mHeadPoint = null;
	}
	public override void setPosition(Vector3 pos) 
	{
		base.setPosition(pos);
		mHPBar?.setPosition(worldToScreen(localToWorld(mModelTransform, Vector3.zero)) + new Vector3(0, 100, 0));
	}
	public Collider getCollider() { return mCollider; }
	public MonsterHPBar getHPBar() { return mHPBar; }
	public Transform getFootPoint()
	{
		if (mFootPoint == null)
		{
			logError("找不到FootPoint, root:" + (mObject != null ? mObject.name : "null"));
		}
		return mFootPoint;
	}
	public Transform getBodyPoint()
	{
		if (mBodyPoint == null)
		{
			logError("找不到BodytPoint, root:" + (mObject != null ? mObject.name : "null"));
		}
		return mBodyPoint; 
	}
	public Transform getHeadPoint()
	{
		if (mHeadPoint == null)
		{
			logError("找不到HeadPoint, root:" + (mObject != null ? mObject.name : "null"));
		}
		return mHeadPoint; 
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void postModelLoaded()
	{
		mHPBar = mUIHPBar.createHPBar();
		mHPBar.setPosition(worldToScreen(localToWorld(mModelTransform, Vector3.zero)) + new Vector3(0, 50, 0));
		// 如果此时怪物数据已经设置,则可刷新血条,没有则在设置数据时再刷新
		CharacterMonsterData monsterData = mMonster.getMonsterData();
		if (monsterData.mTableData != null)
		{
			mHPBar.setPercent(divide(monsterData.mHP, mMonster.getMaxHP()));
		}
		setModelParent(mBattleScene.getMonsterRoot());
		mFootPoint = findGameObject(CHARACTER_FOOT_POINT, mObject, true).transform;
		mBodyPoint = findGameObject(CHARACTER_BODY_POINT, mObject, true).transform;
		// 可以没有头部点
		GameObject headGo = findGameObject(CHARACTER_HEAD_POINT, mObject);
		if (headGo != null)
		{
			mHeadPoint = headGo.transform;
		}
		mObject.TryGetComponent(out mCollider);
		mObject.layer = nameToLayerInt(LAYER_MONSTER);
		mGlobalTouchSystem.registeCollider(mMonster);
		mMonster.setClickCallback(onClick);
		mMonster.setPassRay(false);
	}
	protected void onClick()
	{
		CmdGlobalFocusAttackMonster.execute(mMonster);
	}
}
