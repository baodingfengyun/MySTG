using UnityEngine;
using System.Collections.Generic;
using static FrameBaseHotFix;
using static UnityUtility;
using static FrameBaseUtility;
using static FrameDefine;
using static GDR;

// 大厅的场景
public class MainScene : SceneInstance
{
	protected MovableObject mDefense;			// 防线
	protected MovableObject mFountain;			// 祈福
	protected MovableObject mMapEditor;			// 地图编辑器
	protected MovableObject mHeroTavern;		// 英雄养成酒馆
	protected MovableObject mMine;				// 矿场
	protected MovableObject mTowerResearch;		// 农田
	protected MovableObject mShop;				// 商店
	protected MovableObject mMonument;          // 丰碑
	protected GameObject mBuildingCombines;     // 合并后的物体
	protected MeshRenderer mCombinesRenderer;	// 合并后的渲染组件
	//protected AudioHelper mMainWaterBgm;		// 水流环境音效
	protected AudioHelper mMainBirdsongBgm;		// 鸟鸣环境音效
	protected GameCamera mMainCamera;
	protected Material[] mOriginMaterials;
	protected static string mMaterialPath = R_SCENE_PATH + SCENE_MAIN + "/Res/";
	public static string mDefenseGreyMaterialName = mMaterialPath + "M_fangxian_BW.mat";
	public static string mFountainGreyMaterialName = mMaterialPath + "M_guangchang_BW.mat";
	public static string mShopGreyMaterialName = mMaterialPath + "M_shangdian_BW.mat";
	public static string mMineGreyMaterialName = mMaterialPath + "M_kuangdong_BW.mat";
	public static string mHeroTavernGreyMaterialName = mMaterialPath + "M_jiuguan_BW.mat";
	public static string mTowerResearchGreyMaterialName = mMaterialPath + "M_nongtian_BW.mat";
	public static string mMonumentGreyMaterialName = mMaterialPath + "M_StoneHenge_BW.mat";
	public override void init()
	{
		base.init();
		initGameObject();
        mMainBirdsongBgm = AT.SOUND_2D(SOUND_HOTFIX.MAIN_BIRDSONG_BGM, true);

		mCombinesRenderer = mBuildingCombines.GetComponent<MeshRenderer>();
		mOriginMaterials = mCombinesRenderer.sharedMaterials;
		if (mOriginMaterials.Length != 8)
		{
			logError("建筑材质的数量应该为8个");
		}
	}
	public override void destroy()
	{
		mCameraManager?.destroyCamera(mMainCamera);
		mMovableObjectManager?.destroyObject(ref mDefense);
		mMovableObjectManager?.destroyObject(ref mFountain);
		mMovableObjectManager?.destroyObject(ref mMapEditor);
		mMovableObjectManager?.destroyObject(ref mHeroTavern);
		mMovableObjectManager?.destroyObject(ref mMine);
		mMovableObjectManager?.destroyObject(ref mTowerResearch);
		mMovableObjectManager?.destroyObject(ref mShop);
		mMovableObjectManager?.destroyObject(ref mMonument);
		//AT.SOUND(mMainWaterBgm);
		AT.SOUND(mMainBirdsongBgm);
		mCombinesRenderer = null;
		mOriginMaterials = null;
		mEventSystem?.unlistenEvent(this);
		base.destroy();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mDefense = null;
		mFountain = null;
		mMapEditor = null;
		mHeroTavern = null;
		mMine = null;
		mTowerResearch = null;
		mShop = null;
		mMonument = null;
		mBuildingCombines = null;
		mCombinesRenderer = null;
		//mMainWaterBgm = null;
		mMainBirdsongBgm = null;
		mMainCamera = null;
		mOriginMaterials = null;
	}
	public Vector3 getDefenseNamePos() { return findGameObject("Name", mDefense.getGameObject()).transform.position; }
	public Vector3 getSquareNamePos() { return findGameObject("Name", mFountain.getGameObject()).transform.position; }
	public Vector3 getMapEditorNamePos() { return findGameObject("Name", mMapEditor.getGameObject()).transform.position; }
	public Vector3 getHeroTavernNamePos() { return findGameObject("Name", mHeroTavern.getGameObject()).transform.position; }
	public Vector3 getMineNamePos() { return findGameObject("Name", mMine.getGameObject()).transform.position; }
	public Vector3 getFarmLandNamePos() { return findGameObject("Name", mTowerResearch.getGameObject()).transform.position; }
	public Vector3 getShopNamePos() { return findGameObject("Name", mShop.getGameObject()).transform.position; }
	public Vector3 getMonumentNamePos() { return findGameObject("Name", mMonument.getGameObject()).transform.position; }
	public override void onShow()
	{
		if (mMainCamera != null)
		{
			mCameraManager.setMainCamera(mMainCamera);
		}
	}
	public override void onHide()
	{
		base.onHide();
		mCameraManager.setMainCamera(mCameraManager.getDefaultCamera());
	}
	public void setActiveOnlyTowerResearch(out Vector3 pos)
	{
		mGlobalTouchSystem.setActiveOnlyObject(mTowerResearch);
		pos = worldToScreen(mTowerResearch.getWorldPosition());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void checkActiveMaterial(bool isActive, int index, string greyMaterialName)
	{
		if (mCombinesRenderer == null)
		{
			return;
		}
		if (isActive)
		{
			List<Material> list = new(mCombinesRenderer.sharedMaterials);
			list[index] = mOriginMaterials[index];
			mCombinesRenderer.SetSharedMaterials(list);
		}
		else
		{
			mResourceManager.loadGameResourceAsync(greyMaterialName, (ResourceRef<Material> mat) =>
			{
				if (mCombinesRenderer == null)
				{
					return;
				}
				List<Material> list = new(mCombinesRenderer.sharedMaterials);
				list[index] = mat.get();
				mCombinesRenderer.SetSharedMaterials(list);
			});
		}
	}
	protected void initGameObject()
	{
        mDefense = mMovableObjectManager.createMovableObject(findGameObject("B_FangXian", mRoot));
        mFountain = mMovableObjectManager.createMovableObject(findGameObject("B_GuangChang", mRoot));
        mMapEditor = mMovableObjectManager.createMovableObject(findGameObject("B_MapEditor", mRoot));
        mHeroTavern = mMovableObjectManager.createMovableObject(findGameObject("B_JiuGuan", mRoot));
        mMine = mMovableObjectManager.createMovableObject(findGameObject("B_KuangChang", mRoot));
        mTowerResearch = mMovableObjectManager.createMovableObject(findGameObject("B_NongTian", mRoot));
        mShop = mMovableObjectManager.createMovableObject(findGameObject("B_ShangDian", mRoot));
        mMonument = mMovableObjectManager.createMovableObject(findGameObject("B_FengBei", mRoot));
        mBuildingCombines = findGameObject("CombinedMesh_Buildings", mRoot);

        // 先创建摄像机,再注册点击事件
        mMainCamera = mCameraManager.createCamera("MainCamera", mRoot);
		mCameraManager.setMainCamera(mMainCamera);
		mGlobalTouchSystem.registeCollider(mDefense, mMainCamera);
		mGlobalTouchSystem.registeCollider(mFountain, mMainCamera);
		mGlobalTouchSystem.registeCollider(mMapEditor, mMainCamera);
		mGlobalTouchSystem.registeCollider(mHeroTavern, mMainCamera);
		mGlobalTouchSystem.registeCollider(mMine, mMainCamera);
		mGlobalTouchSystem.registeCollider(mTowerResearch, mMainCamera);
		mGlobalTouchSystem.registeCollider(mShop, mMainCamera);
		mGlobalTouchSystem.registeCollider(mMonument, mMainCamera);
		mHeroTavern.setClickSound(SOUND_HOTFIX.MAIN_SHOP_BUTTON);
		mShop.setClickSound(SOUND_HOTFIX.MAIN_SHOP_BUTTON);
		mMonument.setClickSound(SOUND_HOTFIX.MAIN_SHOP_BUTTON);
	}
}
