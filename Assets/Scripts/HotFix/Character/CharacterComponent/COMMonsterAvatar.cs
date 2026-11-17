using UnityEngine;
using static GDR;
using static GBR;
using static UnityUtility;
using static FrameBaseHotFix;
using static FrameBaseUtility;

// 怪物的显示组件，继承 COMCharacterAvatar，大部分模型加载逻辑由基类提供。
// 怪物特化的部分：模型加载后(postModelLoaded)挂血条、找三个命中点(脚/身/头，头可缺省)、
// 取碰撞体并注册到 GlobalTouchSystem(供点击拾取)、设置 MONSTER 层；血条随 setPosition 跟随移动。
// 点击模型触发 CmdGlobalFocusAttackMonster——把该怪物设为集火目标（呼应 TowerSkill 的索敌优先级）。
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
			mHPBar.setPercent(monsterData.mHP.divide(mMonster.getMaxHP()));
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
	// 点击怪物模型：发命令把此怪设为全塔集火目标。
	// 这是玩家手动集火的入口，TowerSkill.checkSearchTarget 会优先锁定该焦点目标。
	protected void onClick()
	{
		CmdGlobalFocusAttackMonster.execute(mMonster);
	}
}
