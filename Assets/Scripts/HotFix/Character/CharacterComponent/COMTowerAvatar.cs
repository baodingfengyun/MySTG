using UnityEngine;
using static GBR;
using static GDR;
using static FrameBaseHotFix;
using static UnityUtility;
using static MathUtility;
using static FrameBaseUtility;

// 显示塔模型,主要逻辑由基类提供
public class COMTowerAvatar : COMCharacterAvatar
{
	protected CharacterTower mTower;				// 所属的塔
	protected Transform mTowerRotateRoot;           // 塔转向时的节点
	protected Transform mInstallPoint;				// 放置窜天猴或者其他物体的位置节点
	protected GameObject mTowerModel;               // 塔prefab下的模型
	protected CharacterCallback mModelInited;		// 加载完毕后且初始化完成的回调,虽然基类也有一个模型加载完毕的回调,但是需要有一个初始化完成的回调
	protected bool mModelLoaded;					// 模型是否已经加载完毕,因为有替换模型的需求,替换模型过程中认为模型没有加载完毕,然而此时是可以获取到模型的
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mTower = mComponentOwner as CharacterTower;
		mRelationship = AVATAR_RELATIONSHIP.AVATAR_ALONE;
	}
	public override void destroyModel()
	{
		// 还原节点旋转
		if (getModel() != null)
		{
			towerLookPostion(localToWorld(getModel().transform, Vector3.forward));
		}
		mGlobalTouchSystem?.unregisteCollider(mTower);
		mTowerRotateRoot = null;
		mTowerModel = null;
		mModelLoaded = false;
		base.destroyModel();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
		mTowerRotateRoot = null;
		mInstallPoint = null;
		mTowerModel = null;
		mModelInited = null;
		mModelLoaded = false;
	}
	public Collider getCollider()
	{
		if (mObject == null)
		{
			return null;
		}
		mObject.TryGetComponent<Collider>(out var collider);
		return collider;
	}
	public bool isModelLoaded() { return mModelLoaded; }
	public void setModelLoaded(bool loaded) { mModelLoaded = loaded; }
	public Transform getTowerRotateRoot() { return mTowerRotateRoot; }
	public Transform getInstallPoint() { return mInstallPoint; }
	public GameObject getTowerModel() { return mTowerModel; }
	public void towerLookPostion(Vector3 pos)
	{
		if (mTowerRotateRoot != null)
		{
			mTowerRotateRoot.LookAt(replaceY(pos, mTowerRotateRoot.position.y));
		}
	}
	public void setModelInitedCallback(CharacterCallback callback)
	{
		if (mModelLoaded)
		{
			callback?.Invoke(mTower);
		}
		else
		{
			mModelInited += callback;
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void postModelLoaded()
	{
		base.postModelLoaded();
		setModelParent(mBattleScene.getTowerRoot());
		// 存储塔的转向节点
		string rotateRootName = mTower.getTowerData().mTableData.mRotateRoot;
		if (!rotateRootName.isEmpty())
		{
			GameObject rotateGo = findGameObject(rotateRootName, mObject, true);
			if (rotateGo != null)
			{
				mTowerRotateRoot = rotateGo.transform;
			}
		}
		if (mObject.transform.childCount > 0 && mObject.transform.GetChild(0) != null)
		{
			mTowerModel = mObject.transform.GetChild(0).gameObject;
		}
		GameObject installGo = findGameObject(INSTALL_POINT, mObject);
		if (installGo != null)
		{
			mInstallPoint = installGo.transform;
		}
		// 模型加载完以后才能注册点击事件,因为这时候才会有碰撞体
		mGlobalTouchSystem.registeCollider(mTower);
		mTower.setClickCallback(onClick);
		mTower.setPassRay(false);
		mModelLoaded = true;
		mModelInited?.Invoke(mTower);
		mModelInited = null;
	}
	protected void onClick()
	{
		// 再次点击已选中的塔时,取消选中
		if (mTowerDefenceSystem.getSelectedTowerScene() == mTower)
		{
			CmdGlobalSelectTowerScene.execute(null);
		}
		// 点击防御塔显示防御塔攻击范围
		else
		{
			CmdGlobalSelectTowerScene.execute(mTower);
		}
	}
}