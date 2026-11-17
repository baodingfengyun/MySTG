using UnityEngine;
using System.Collections.Generic;
using static FrameBaseHotFix;
using static UnityUtility;
using static FrameBaseUtility;
using static FrameDefine;
using static GDR;

// 大厅(主界面)场景脚本,是 SceneInstance 的子类之一,由 SceneRegister.registerAll 登记。
// 大厅里分布着若干功能建筑(防线、祈福广场、地图编辑器、酒馆、矿场、农田、商店、丰碑),
// 每个建筑是一个可点击的 MovableObject,点击后由全局触摸系统派发到对应 UI 逻辑。
// 生命周期(init/onShow/onHide/destroy/resetProperty/update)均由框架 SceneSystem 驱动,
// 本类只在各回调里管理"建筑对象 + 相机 + 环境音效 + 合并网格材质"这些场景专属资源。
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
	// 灰度材质路径前缀:指向大厅场景资源目录下的 Res 子目录,存放各建筑的黑白(_BW)材质。
	// 下面一组 *GreyMaterialName 分别对应防线/广场/商店/矿场/酒馆/农田/丰碑,
	// 用于建筑未解锁时把合并网格对应槽位替换为灰度;实际切换逻辑见 checkActiveMaterial。
	protected static string mMaterialPath = R_SCENE_PATH + SCENE_MAIN + "/Res/";
	public static string mDefenseGreyMaterialName = mMaterialPath + "M_fangxian_BW.mat";
	public static string mFountainGreyMaterialName = mMaterialPath + "M_guangchang_BW.mat";
	public static string mShopGreyMaterialName = mMaterialPath + "M_shangdian_BW.mat";
	public static string mMineGreyMaterialName = mMaterialPath + "M_kuangdong_BW.mat";
	public static string mHeroTavernGreyMaterialName = mMaterialPath + "M_jiuguan_BW.mat";
	public static string mTowerResearchGreyMaterialName = mMaterialPath + "M_nongtian_BW.mat";
	public static string mMonumentGreyMaterialName = mMaterialPath + "M_StoneHenge_BW.mat";
	// 场景加载完成后由框架 SceneSystem 回调。先走基类初始化,再创建场景物体、播放环境鸟鸣,
	// 并缓存合并网格的原始彩色材质数组(约定恰好 8 个槽位,对应 8 个功能建筑)。
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
	// 场景卸载时由框架回调:销毁相机与所有建筑 MovableObject、停止环境音、取消事件监听,
	// 最后回到基类清理。此处统一用 ?. 防止异常路径下字段为空导致崩溃。
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
	// 对象池回收复用时把所有引用清空(置 null),与 init 配对,避免持有已销毁的 Unity 对象。
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
	// 下面一组方法返回各建筑节点下名为"Name"的子节点世界坐标,供 UI 在建筑上方浮动显示名称。
	public Vector3 getDefenseNamePos() { return findGameObject("Name", mDefense.getGameObject()).transform.position; }
	public Vector3 getSquareNamePos() { return findGameObject("Name", mFountain.getGameObject()).transform.position; }
	public Vector3 getMapEditorNamePos() { return findGameObject("Name", mMapEditor.getGameObject()).transform.position; }
	public Vector3 getHeroTavernNamePos() { return findGameObject("Name", mHeroTavern.getGameObject()).transform.position; }
	public Vector3 getMineNamePos() { return findGameObject("Name", mMine.getGameObject()).transform.position; }
	public Vector3 getFarmLandNamePos() { return findGameObject("Name", mTowerResearch.getGameObject()).transform.position; }
	public Vector3 getShopNamePos() { return findGameObject("Name", mShop.getGameObject()).transform.position; }
	public Vector3 getMonumentNamePos() { return findGameObject("Name", mMonument.getGameObject()).transform.position; }
	// 场景被显示时回调:把本场景自带的相机设为游戏主相机。
	public override void onShow()
	{
		if (mMainCamera != null)
		{
			mCameraManager.setMainCamera(mMainCamera);
		}
	}
	// 场景被隐藏时回调:把主相机恢复为框架默认相机。
	public override void onHide()
	{
		base.onHide();
		mCameraManager.setMainCamera(mCameraManager.getDefaultCamera());
	}
	// 新手引导用:只允许点击农田建筑,并输出其屏幕坐标供 UI 定位引导手/箭头。
	public void setActiveOnlyTowerResearch(out Vector3 pos)
	{
		mGlobalTouchSystem.setActiveOnlyObject(mTowerResearch);
		pos = worldToScreen(mTowerResearch.getWorldPosition());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	// 切换合并网格第 index 个材质槽的彩色/灰度:isActive=true 恢复原始彩色材质,
	// false 则异步加载灰度材质替换。异步回调内再次判空,防止加载期间场景已销毁而空引用。
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
	// 从场景根节点 mRoot 下按固定名称查找并创建各建筑 MovableObject、合并网格与相机;
	// 遵循"先创建相机,再注册点击碰撞体"的顺序,并为酒馆/商店/丰碑设置点击音效。
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
