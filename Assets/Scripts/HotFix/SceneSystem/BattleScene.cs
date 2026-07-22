using System;
using System.Collections.Generic;
using UnityEngine;
using static GameUtilityHotFix;
using static FrameBaseUtility;
using static FrameUtility;
using static UnityUtility;
using static MathUtility;
using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 处理仅与战斗场景资源相关的逻辑
public class BattleScene : SceneInstance
{
	protected Dictionary<int, MovableObject> mGridObjectList = new();           // 格子的基础显示物体
	protected Dictionary<int, MovableObject> mGridObjectModelList = new();      // 格子的显示物体模型
	protected Dictionary<int, Material> mGridObjectModelMaterialList = new();   // 格子的显示物体模型的材质
	protected Dictionary<int, Renderer> mGridRendererList = new();				// 格子基础显示物体上的渲染对象
	protected List<List<Vector3>> mMovePathPoint = new();						// 路线的点位
	protected List<List<GameEffect>> mMovePathEffect = new();					// 路线动效
	protected List<myLineRenderer> mPreviewPathRenderer = new();				// 用于显示预览路线
	protected List<Vector2> mHexPoints = new();									// 六边形的六个顶点坐标
	protected GameObject mMonsterRoot;											// 所有怪物的渲染节点的父节点
	protected GameObject mTowerRoot;											// 所有防御塔的渲染节点的父节点
	protected GameObject mBattlePropRoot;										// 所有战斗道具的渲染节点的父节点
	protected GameObject mGridRoot;												// 所有格子渲染节点的父节点
	protected GameObject mRemoveableRoot;										// 所有可移除物件的渲染节点的父节点
	protected GameObject mTimeLine;												// timeline的节点,不一定每个场景都有
	protected GameObject mHomePoint;											// 村长家的节点
	protected GameObject mTowerRangeEffect;										// 塔范围的特效
	protected GameObject mTowerMinRangeEffect;									// 塔不能攻击范围的特效 ，暂时只有投石机有
	protected GameObject mSkillRangeEffect;										// 技能范围的特效
	protected GameEffect mSelectingEffect;										// 塔选中特效
	protected GameEffect mEndPointEffect;										// 终点的特效
	protected GameEffect mGridDragTipArrow;										// 引导拖拽格子的箭头
	protected MovableObject mTerrain;											// 地面物体
	protected Material mRectWalkableMaterial;									// 正方形格子可行走的格子材质
	protected Material mRectRedMaterial;										// 正方形格子红色的格子材质
	protected Material mRectGreenMaterial;										// 正方形格子绿色的格子材质
	protected Material mRectBlockMaterial;										// 正方形格子不可行走也不可摆放的格子材质
	protected Material mHexWalkableMaterial;									// 正六边形格子可行走的格子材质
	protected Material mHexRedMaterial;											// 正六边形格子红色的格子材质
	protected Material mHexGreenMaterial;										// 正六边形格子绿色的格子材质
	protected Material mHexBlockMaterial;										// 正六边形格子不可行走也不可摆放的格子材质
	protected Material mHexDragTipMaterial;										// 正六边形格子引导提示拖拽的格子材质
	protected Vector3 mGridOriginScale;											// 格子初始的缩放值
	protected float mPathLineTimer;												// 每隔一段时间生成一个路径粒子特效
	protected int mDragOnlyGrid = -1;											// 只允许拖拽到的格子下标,用于新手引导
	protected bool mCanReplaceTower = true;										// 是否禁止替换塔，用于新手引导，随意放置塔的位置时
	protected float mCameraMaxHeight;											// 镜头最高点
	protected float mCameraMinHeight;											// 镜头最低点
	protected Vector3 mCameraMinIntersect;										// 镜头在y=0平面上的最小视锥交点
	protected Vector3 mCameraMaxIntersect;										// 镜头在y=0平面上的最大视锥交点
	protected Vector3 mCameraInitPos;											// 镜头初始位置
	public BattleScene()
	{
		// 从最上面的点开始,顺时针计算点坐标
		mHexPoints.Add(new(0.0f, HEX_CORNER_DISTANCE * 0.5f));
		mHexPoints.Add(new(GRID_SIZE * 0.5f, HEX_EDGE_LENGTH * 0.5f));
		mHexPoints.Add(new(GRID_SIZE * 0.5f, -HEX_EDGE_LENGTH * 0.5f));
		mHexPoints.Add(new(0.0f, -HEX_CORNER_DISTANCE * 0.5f));
		mHexPoints.Add(new(-GRID_SIZE * 0.5f, -HEX_EDGE_LENGTH * 0.5f));
		mHexPoints.Add(new(-GRID_SIZE * 0.5f, HEX_EDGE_LENGTH * 0.5f));
	}
	public override void init()
	{
		base.init();
		initGameObject();
        mGlobalTouchSystem.registeCollider(mTerrain);
		mTerrain.setClickCallback(onTerrainClick);

		// 创建摄像机
		GameCamera gameCamera = mCameraManager.createCamera("MainCamera", mRoot);
		mCameraManager.setMainCamera(gameCamera);
		gameCamera.setPosition(mCameraInitPos);
		Camera cam = gameCamera.getCamera();
		// 获取摄像机初始视野范围 (使用视锥体边界)
		Vector3 bottomLeftWorld = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
		Vector3 topRightWorld = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
		// 相机
		mCameraInitPos = cam.transform.localPosition;
		mCameraMinIntersect = intersectRayPlane(new Ray(mCameraInitPos, bottomLeftWorld - mCameraInitPos), Vector3.up, Vector3.zero);
		mCameraMaxIntersect = intersectRayPlane(new Ray(mCameraInitPos, topRightWorld - mCameraInitPos), Vector3.up, Vector3.zero);
	}
	public void loadResourceAsync(Action callback)
	{
		AsyncTaskGroup group = mAsyncTaskGroupManager.createGroup(callback);
		// 加载需要用到的材质
		string towerRangeEffect = EDEffect.TOWER_RANGE.mPath;
		string towerMinRangeEffect = EDEffect.TOWER_MIN_RANGE.mPath;
		string skillRangeEffect = EDEffect.SKILL_RANGE.mPath;
		group.addTask(mPrefabPoolManager.createObjectAsync(towerRangeEffect, true, true, (res) => { res.transform.parent = mRoot.transform; mTowerRangeEffect = res; }));
		group.addTask(mPrefabPoolManager.createObjectAsync(towerMinRangeEffect, true, true, (res) => { res.transform.parent = mRoot.transform; mTowerMinRangeEffect = res; }));
		group.addTask(mPrefabPoolManager.createObjectAsync(skillRangeEffect, true, true, (res) => { res.transform.parent = mRoot.transform; mSkillRangeEffect = res; }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(RECT_WALKABLE_MAT, (res) => { mRectWalkableMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(RECT_RED_MAT, (res) => { mRectRedMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(RECT_GREEN_MAT, (res) => { mRectGreenMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(RECT_BLOCK_MAT, (res) => { mRectBlockMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(HEX_WALKABLE_MAT, (res) => { mHexWalkableMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(HEX_RED_MAT, (res) => { mHexRedMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(HEX_GREEN_MAT, (res) => { mHexGreenMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(HEX_BLOCK_MAT, (res) => { mHexBlockMaterial = res.get(); }));
		group.addTask(mResourceManager.loadGameResourceAsync<Material>(DRAG_TIP_MAT, (res) => { mHexDragTipMaterial = res.get(); }));
		group.addTask(mPrefabPoolManager.initObjectToPoolAsync(EDEffect.BATTLE_MOVE_PATH.mPath, 0, false, null));
		group.addTask(mPrefabPoolManager.initObjectToPoolAsync(EDEffect.BATTLE_PATH.mPath, 0, true, null));
		group.addTask(mPrefabPoolManager.initObjectToPoolAsync(EDEffect.BATTLE_PATH_PREVIEW.mPath, 0, true, null));

		// 加载格子资源,初始化格子属性
		Vector3 gridRootPos = mTowerDefenceSystem.getGridRootPos();
		int mapWidth = mTowerDefenceSystem.getLevelWidth();
		int mapHeight = mTowerDefenceSystem.getLevelHeight();
		GRID_TYPE gridType = mTowerDefenceSystem.getGridType();

		int gridCount = mapWidth * mapHeight;
		if (gridType == GRID_TYPE.FOUR)
		{
			float halfAllWidth4D = mapWidth * GRID_SIZE * 0.5f;
			float halfAllHeight4D = mapHeight * GRID_SIZE * 0.5f;
			for (int i = 0; i < gridCount; ++i)
			{
				int indexX = indexToX(i, mapWidth);
				int indexY = indexToY(i, mapWidth);
				int index = i;
				group.addTask(mPrefabPoolManager.createObjectAsync(GRID_PREFAB_4D, true, true, (go) =>
				{
					float x = GRID_SIZE * 0.5f + indexX * GRID_SIZE - halfAllWidth4D;
					float z = halfAllHeight4D - GRID_SIZE * 0.5f - indexY * GRID_SIZE;
					go.transform.localPosition = new Vector3(x, 0.05f, z) + gridRootPos;
					go.transform.localScale = new(GRID_SIZE, 0.0f, GRID_SIZE);
					createGridBaseObject(go, index);
				}));
			}
		}
		else if (gridType == GRID_TYPE.SIX)
		{
			int sceneTheme = mTowerDefenceSystem.getMapTheme();
			float halfAllWidth6D = mapWidth * (GRID_SIZE + 0.5f) * 0.5f;
			float halfAllHeight6D = (mapHeight * HEX_EDGE_LENGTH + (mapHeight + 1) * HEX_INTERSECT_LENGTH) * 0.5f;
			for (int i = 0; i < gridCount; ++i)
			{
				// 先查找节点,找不到再生成
				GameObject go = findGameObject("Grid" + i.IToS(), mGridRoot, false, false);
				if (go == null)
				{
					int indexX = indexToX(i, mapWidth);
					int indexY = indexToY(i, mapWidth);
					int index = i;
					// 奇数行需要向右移动一点,这样才能紧密排列
					float x = HEX_EDGE_LENGTH + indexX * GRID_SIZE - halfAllWidth6D + (indexY & 1) * GRID_SIZE * 0.5f;
					float z = halfAllHeight6D - HEX_EDGE_LENGTH - (indexY * HEX_CORNER_DISTANCE - indexY * HEX_INTERSECT_LENGTH);
					Vector3 gridPos = new Vector3(x, 0.05f, z) + gridRootPos;
					group.addTask(mPrefabPoolManager.createObjectAsync(GRID_PREFAB_6D, true, true, (go) =>
					{
						go.transform.localPosition = gridPos + new Vector3(0.0f, 0.05f);
						go.transform.localEulerAngles = replaceY(go.transform.localEulerAngles, 30.0f);
						// 稍微放大一些,避免由于计算精度导致的格子之间的空隙
						go.transform.localScale = new(GRID_SIZE + 0.01f, 1.0f, GRID_SIZE + 0.01f);
						createGridBaseObject(go, index);
					}));
					EDGridPrefab prefabInfo = mExcelGridPrefab.getRandomPrefab(mTowerDefenceSystem.getGridState(index), sceneTheme);
					if (prefabInfo != null)
					{
						group.addTask(mPrefabPoolManager.createObjectAsync(prefabInfo.mPrefab, true, true, (go) =>
						{
							go.name = "Grid" + index.IToS() + "_Prefab";
							go.transform.parent = mGridRoot.transform;
							MovableObject gridPrefabObj = mGridObjectModelList.add(index, mMovableObjectManager.createMovableObject(go));
							gridPrefabObj.setPosition(gridPos);
							gridPrefabObj.setRotationY(randomInt(0, 5) * 60);
						}));
						group.addTask(mResourceManager.loadGameResourceAsync<Material>(prefabInfo.mMaterial, (material, _, _, _) =>
						{
							mGridObjectModelMaterialList.Add(index, material.get());
						}));
					}
				}
				// 场景中已经存在了格子节点
				else
				{
					createGridBaseObject(go, i);
				}
			}
		}
	}
	public void initData()
	{
		// 加载好所有资源后，再给模型加材质
		int levelWidth = mTowerDefenceSystem.getLevelWidth();
		int levelHeight = mTowerDefenceSystem.getLevelHeight();
		GRID_TYPE gridType = mTowerDefenceSystem.getGridType();
		int gridCount = levelWidth * levelHeight;
		if (gridType == GRID_TYPE.FOUR)
		{
			for (int i = 0; i < gridCount; ++i)
			{
				mGridRendererList.get(i).material = mTowerDefenceSystem.getGridState(i) == GRID_STATE.BLOCK ? mRectBlockMaterial : mRectWalkableMaterial;
			}
		}
		else if (gridType == GRID_TYPE.SIX)
		{
			for (int i = 0; i < gridCount; ++i)
			{
				mGridRendererList.get(i).material = mTowerDefenceSystem.getGridState(i) == GRID_STATE.BLOCK ? mHexBlockMaterial : mHexWalkableMaterial;
			}
		}
		for (int i = 0; i < gridCount; ++i)
		{
			// 根据Grid_Prefab是否存在来决定是否显示Grid基础模型
			mGridRendererList.get(i).enabled = findGameObject("Grid" + i.IToS() + "_Prefab", mGridRoot) != null;
		}
		foreach (var each in mGridObjectModelMaterialList)
		{
			mGridObjectModelList.get(each.Key).getGameObject().GetComponentInChildren<Renderer>().sharedMaterial = each.Value;
		}
		
		// 隐藏机制显示的物品
		mTowerRangeEffect.SetActive(false);
		mTowerMinRangeEffect.SetActive(false);
		mSkillRangeEffect.SetActive(false);
		if (mEndPointEffect != null)
		{
			logError("终点特效未销毁");
		}
		mEffectManager.createEffectAsyncSafe(EDEffect.END_POINT.mPath, this, null, true, (GameEffect effect) =>
		{
			mEndPointEffect = effect;
			int targetIndex = mTowerDefenceSystem.getBattleModeInstance().getTargetPointIndex();
			mEndPointEffect.setPosition(getGridPosition(targetIndex));
			mEndPointEffect.play();
		}, 0);

		// 创建路线特效
		int pathCount = mTowerDefenceSystem.getMonsterRoadList().Count;
		for (int i = 0; i < pathCount; ++i)
		{
			GameObject previewObject = mPrefabPoolManager.createObject(EDEffect.BATTLE_PATH_PREVIEW.mPath, true, true);
			myLineRenderer previewPathLine = mPreviewPathRenderer.add(new());
			previewPathLine.setLineRenderer(previewObject.GetComponent<LineRenderer>());
			previewPathLine.setActive(false);
			mMovePathEffect.Add(new());
			mMovePathPoint.Add(new());
		}
	}
	public override void destroy()
	{
		foreach (MovableObject obj in mGridObjectList.Values)
		{
			GameObject go = obj.getUnityObject();
			mMovableObjectManager.destroyObject(obj);
			// mGridObjectModelList中有元素表示是自动生成的,没有则表示是查找的已经存在的节点
			if (mGridObjectModelList.Count > 0)
			{
				mPrefabPoolManager.destroyObject(ref go, false);
			}
		}
		foreach (MovableObject obj in mGridObjectModelList.Values)
		{
			GameObject go = obj.getUnityObject();
			mMovableObjectManager.destroyObject(obj);
			mPrefabPoolManager.destroyObject(ref go, false);
		}
		foreach (myLineRenderer each in mPreviewPathRenderer)
		{
			mPrefabPoolManager.destroyObject(each.getGameObject(), false);
			each.setLineRenderer(null);
		}
		foreach (List<GameEffect> effects in mMovePathEffect)
		{
			foreach (GameEffect item in effects)
			{
				mEffectManager.destroyEffect(item);
			}
		}
		mGridObjectList.Clear();
		mGridObjectModelList.Clear();
		mGridObjectModelMaterialList.Clear();
		mMovePathEffect.Clear();
		mPreviewPathRenderer.Clear();
		mPrefabPoolManager?.destroyObject(ref mTowerRangeEffect, false);
		mPrefabPoolManager?.destroyObject(ref mTowerMinRangeEffect, false);
		mPrefabPoolManager?.destroyObject(ref mSkillRangeEffect, false);
		mMovableObjectManager?.destroyObject(ref mTerrain);
		mCameraManager?.destroyCamera(getMainCamera());
		mCameraManager?.activeCamera(getMainCamera(), true);
		mEffectManager?.destroyEffect(ref mSelectingEffect);
		mEffectManager?.destroyEffect(ref mEndPointEffect);
		mEffectManager?.destroyEffect(ref mGridDragTipArrow);
		base.destroy();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mGridObjectList.Clear();
		mGridObjectModelList.Clear();
		mGridObjectModelMaterialList.Clear();
		mGridRendererList.Clear();
		mMovePathPoint.Clear();
		mMovePathEffect.Clear();
		mPreviewPathRenderer.Clear();
		mHexPoints.Clear();
		mMonsterRoot = null;
		mTowerRoot = null;
		mBattlePropRoot = null;
		mGridRoot = null;
		mRemoveableRoot = null;
		mHomePoint = null;
		mTimeLine = null;
		mTowerRangeEffect = null;
		mTowerMinRangeEffect = null;
		mSkillRangeEffect = null;
		mSelectingEffect = null;
		mEndPointEffect = null;
		mGridDragTipArrow = null;
		mTerrain = null;
		mRectWalkableMaterial = null;
		mRectRedMaterial = null;
		mRectGreenMaterial = null;
		mRectBlockMaterial = null;
		mHexWalkableMaterial = null;
		mHexRedMaterial = null;
		mHexGreenMaterial = null;
		mHexBlockMaterial = null;
		mHexDragTipMaterial = null;
		mGridOriginScale = Vector3.zero;
		mPathLineTimer = 0.0f;
		mDragOnlyGrid = -1;
		mCanReplaceTower = true;
		mCameraMaxHeight = 0.0f;
        mCameraMinHeight = 0.0f;
        mCameraMinIntersect = Vector3.zero;
        mCameraMaxIntersect = Vector3.zero;
        mCameraInitPos = Vector3.zero;
    }
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (tickTimerLoop(ref mPathLineTimer, elapsedTime, BATTLE_PATH_EFFECT_INTERVAL_TIME))
		{
			int movePathPointCount = mMovePathPoint.Count;
			for (int i = 0; i < movePathPointCount; ++i)
			{
				startPathEffect(i, mMovePathPoint[i]);
			}
		}
	}
	// 获取世界坐标在忽略Y轴的情况下落在哪个格子中,如果之前已经计算过了格子下标,则优先判断此格子
	public int worldPointToGridIndex(Vector3 point, int lastGridIndex = -1)
	{
		GRID_TYPE gridType = mTowerDefenceSystem.getGridType();
		if (gridType == GRID_TYPE.FOUR)
		{
			// point是世界的坐标,需要转换到GridRoot下的本地坐标,再加上GridRoot的一半大小的偏移
			int levelWidth = mTowerDefenceSystem.getLevelWidth();
			int levelHeight = mTowerDefenceSystem.getLevelHeight();
			point = worldToLocal(mGridRoot.transform, point);
			point.x += levelWidth * GRID_SIZE * 0.5f;
			point.z = -point.z + levelHeight * GRID_SIZE * 0.5f;
			// 根据射线交点计算出所处的块下标
			int x = (int)(point.x / GRID_SIZE);
			if (x < 0 || x >= levelWidth)
			{
				return -1;
			}
			int y = (int)(point.z / GRID_SIZE);
			if (y < 0 || y >= levelHeight)
			{
				return -1;
			}
			return x + y * levelWidth;
		}
		else if (gridType == GRID_TYPE.SIX)
		{
			Vector2 point2D = new(point.x, point.z);
			if (lastGridIndex >= 0)
			{
				Vector3 center0 = mGridObjectList.get(lastGridIndex).getPosition();
				if (isPointInPolygon(mHexPoints, point2D - new Vector2(center0.x, center0.z)))
				{
					return lastGridIndex;
				}
			}
			// 六边形就直接使用射线检测,看射线与哪个六边形相交
			int count = mGridObjectList.Count;
			for (int i = 0; i < count; ++i)
			{
				Vector3 center = mGridObjectList.get(i).getPosition();
				if (isPointInPolygon(mHexPoints, point2D - new Vector2(center.x, center.z)))
				{
					return i;
				}
			}
		}
		return -1;
	}
	// 获取屏幕上鼠标的点与格子的交点世界坐标
	public bool getMouseRayIntersectGrid(Vector3 screenPos, out Vector3 point)
	{
		return raycast(getMainCameraRay(screenPos), out _, out point, MASK_LEVEL_GRID);
	}
	// 获取屏幕上鼠标点击到的格子下标
	public bool getMouseGridIndex(Vector3 screenPos, out int index)
	{
		index = -1;
		if (getMouseRayIntersectGrid(screenPos, out Vector3 point))
		{
			index = worldPointToGridIndex(point);
		}
		return index != -1;
	}
	// 获取世界坐标垂直投影到地面的位置
	public bool getWorldPositionDownToTerrain(Vector3 pos, out Vector3 point)
	{
		return raycast(new Ray(replaceY(pos, 1000.0f), Vector3.down), out _, out point, MASK_TERRAIN);
	}
	// 获取屏幕上鼠标的点与地面的交点世界坐标
	public bool getMouseRayIntersectTerrain(Vector3 screenPos, out Vector3 point)
	{
		return raycast(getMainCameraRay(screenPos), out _, out point, MASK_TERRAIN);
	}
	// 根据屏幕坐标,获得所在的格子下标以及地面上的点坐标
	public bool getMouseGridIndexAndPoint(Vector3 screenPos, out int index, out Vector3 point)
	{
		if (getMouseRayIntersectGrid(screenPos, out point))
		{
			index = worldPointToGridIndex(point);
			return true;
		}
		else
		{
			index = -1;
			getMouseRayIntersectTerrain(screenPos, out point);
			return false;
		}
	}
	// 新手引导中显示拖拽提示的箭头特效
	public void setGridDragTipArrow(int index)
	{
		if (index < 0)
		{
			mGridDragTipArrow?.setActive(false);
			return;
		}
		if (mGridDragTipArrow == null)
		{
			mEffectManager.createEffectAsyncSafe(GRID_DRAG_TIP_ARROW, this, null, true, (effect) =>
			{
				mGridDragTipArrow = effect;
				mGridDragTipArrow.setPosition(getGridPosition(index));
			}, 0);
			return;
		}
		mGridDragTipArrow.setPosition(getGridPosition(index));
	}
	// 设置一个格子的渲染材质,并且重置其他格子的材质,阻挡类型的格子不能设置
	public void setGridMaterial(int index, Material mat)
	{
		GRID_TYPE dirType = mTowerDefenceSystem.getGridType();
		List<LevelGrid> gridList = mTowerDefenceSystem.getGridList();
		int count = mGridRendererList.Count;
		if (dirType == GRID_TYPE.FOUR)
		{
			for (int i = 0; i < count; ++i)
			{
				Renderer renderer = mGridRendererList.get(i);
				if (gridList[i].getState() == GRID_STATE.BLOCK)
				{
					continue;
				}
				if (i == index)
				{
					renderer.sharedMaterial = mat;
				}
				else
				{
					if (i != mDragOnlyGrid && renderer.sharedMaterial != mRectWalkableMaterial)
					{
						renderer.sharedMaterial = mRectWalkableMaterial;
					}
				}
			}
		}
		else if (dirType == GRID_TYPE.SIX)
		{
			for (int i = 0; i < count; ++i)
			{
				Renderer renderer = mGridRendererList.get(i);
				if (gridList[i].getState() == GRID_STATE.BLOCK)
				{
					continue;
				}
				if (i == index)
				{
					renderer.sharedMaterial = mat;
				}
				else
				{
					Material willSetMat = i == mDragOnlyGrid ? mHexDragTipMaterial : mHexWalkableMaterial;
					if (renderer.sharedMaterial != willSetMat)
					{
						renderer.sharedMaterial = willSetMat;
					}
				}
			}
		}
	}
	public Material getRedMaterial()
	{
		GRID_TYPE dirType = mTowerDefenceSystem.getGridType();
		if (dirType == GRID_TYPE.FOUR)
		{
			return mRectRedMaterial;
		}
		else if (dirType == GRID_TYPE.SIX)
		{
			return mHexRedMaterial;
		}
		return null;
	}
	public Material getGreenMaterial()
	{
		GRID_TYPE dirType = mTowerDefenceSystem.getGridType();
		if (dirType == GRID_TYPE.FOUR)
		{
			return mRectGreenMaterial;
		}
		else if (dirType == GRID_TYPE.SIX)
		{
			return mHexGreenMaterial;
		}
		return null;
	}
	public void setDragOnlyGrid(int index)		{ mDragOnlyGrid = index; }
	public void setCanReplaceTower(bool canReplace)	{ mCanReplaceTower = canReplace; }
	public GameObject getMonsterRoot()			{ return mMonsterRoot; }
	public GameObject getTowerRoot()			{ return mTowerRoot; }
	public GameObject getBattlePropRoot()		{ return mBattlePropRoot; }
	public GameObject getRemoveableRoot()		{ return mRemoveableRoot; }
	public GameObject getTimeline()				{ return mTimeLine; }
	public Vector3 getMaxCameraPos()			{ return mCameraInitPos; }
	public Vector3 getMinCameraPos()			{ return mCameraInitPos + getMainCamera().getForward() * mCameraMaxHeight; }
	public Vector3 getHomePosition()			{ return mHomePoint != null ? mHomePoint.transform.position : Vector3.zero; }
	public int getDragOnlyGrid()				{ return mDragOnlyGrid; }
	public bool canReplaceTower()				{ return mCanReplaceTower; }
	public bool isCameraScaled()				{ return !isFloatEqual(getMainCamera().getPosition().y, mCameraMaxHeight); }
	// 获取一个格子的世界坐标
	public Vector3 getGridPosition(int index) { return mGridObjectList.get(index)?.getWorldPosition() ?? Vector3.zero; }
	public Vector3 getFocusTerrainPosition()
	{
		Ray ray = new(getMainCamera().getPosition(), getMainCamera().getForward());
		return intersectRayPlane(ray, Vector3.up, Vector3.zero);
	}
	// 显示怪物的实际寻路路线
	public void showPath(int roadIndex, List<int> path)
	{
		List<Vector3> pathPoints = mMovePathPoint[roadIndex];
		pathPoints.Clear();
		foreach (int item in path)
		{
			pathPoints.Add(getGridPosition(item));
		}
		foreach (GameEffect effect in mMovePathEffect[roadIndex])
		{
			effect.setActive(false);
            effect.MOVE_CURVE();
		}
		mPathLineTimer = 0.0f;
	}
	// 显示怪物的预览寻路路线,一般是在拖拽防御塔时显示的额外路线
	public void showPreviewPath(int roadIndex, List<int> path)
	{
		mPreviewPathRenderer[roadIndex].setActive(true);
		setPathPoints(mPreviewPathRenderer[roadIndex], path);
	}
	public void showAllPath()
	{
		var roadList = mTowerDefenceSystem.getMonsterRoadList();
		int roadListCount = roadList.Count;
		for (int i = 0; i < roadListCount; ++i)
		{
			showPath(i, roadList[i].mMonsterWalkRoadPoint);
		}
	}
	public void hideAllPreviewPath()
	{
		foreach (myLineRenderer item in mPreviewPathRenderer)
		{
			item.setActive(false);
		}
	}
	// 显示一个防御塔的攻击范围
	public void showTowerRange(CharacterTower tower, int gridIndex = -1)
	{
		// 塔有效则没有指定下标时就自动获取塔的下标
		if (gridIndex < 0 && tower != null)
		{
			gridIndex = tower.getTowerData().mGridIndex;
		}
		mTowerRangeEffect.SetActive(tower != null);
		mTowerMinRangeEffect.SetActive(false);
		// 塔无效就不再执行
		if (tower == null)
		{
			return;
		}
		List<int> hexRange = tower.getTowerData().mTableData.mHexRange;
		GRID_TYPE type = mTowerDefenceSystem.getGridType();
		if (type == GRID_TYPE.SIX && !hexRange.isEmpty())
		{
			// 恢复所有格子的材质
			setGridMaterial(-1, null);
			int range = round(tower.getTowerData().mTableData.mRange);
			using var a = new ListScope<int>(out var grids);
			getHexAroundGird(gridIndex, range, grids);
			int length = grids.Count;
			for (int i = 0; i < length; ++i)
			{
				int grid = grids[i];
				if (grid >= 0 && hexRange.Contains(i % 6) && mTowerDefenceSystem.getGridList()[grid].getState() != GRID_STATE.BLOCK)
				{
					mGridRendererList.get(grid).sharedMaterial = getGreenMaterial();
				}
			}
			mTowerRangeEffect.SetActive(false);
		}
		else if (type == GRID_TYPE.FOUR || type == GRID_TYPE.SIX)
		{
			Vector3 pos = mBattleScene.getGridPosition(gridIndex) + new Vector3(0.0f, 0.01f, 0.0f);
			// 投石机单独拥有一个最小的范围，需要特殊判断
			var toushijiSkill = tower.getComSkill().getCurSkill() as TowerSkill_TouShiJi;
			mTowerMinRangeEffect.SetActive(toushijiSkill != null);
			if (toushijiSkill != null)
			{
				float minRange = toushijiSkill.getMinRange();
				mTowerMinRangeEffect.transform.localPosition = pos;
				mTowerMinRangeEffect.transform.localScale = new(minRange, 1.0f, minRange);
			}
			// 范围为1时缩放为1
			float range = tower.getRange();
			mTowerRangeEffect.SetActive(true);
			mTowerRangeEffect.transform.localPosition = pos;
			mTowerRangeEffect.transform.localScale = new(range, 1.0f, range);
		}
	}
	// 突出显示或者取消突出显示当前可行走且没有塔的格子
	public void showWalkableGrid(bool show)
	{
		if (show)
		{
			int count = mGridObjectList.Count;
			for (int i = 0; i < count; ++i)
			{
				if (canGridStatePlaceTower(mTowerDefenceSystem.getGridState(i)) && !mTowerDefenceSystem.hasItemAtGrid(i))
				{
					Vector3 targetScale = new(mGridOriginScale.x * 0.9f, mGridOriginScale.y, mGridOriginScale.z * 0.9f);
                    mGridObjectList.get(i).SCALE(KEY_CURVE.ZERO_ONE_ZERO, mGridOriginScale, targetScale, 1.0f, true);
				}
			}
		}
		else
		{
			foreach (MovableObject item in mGridObjectList.Values)
			{
				item.SCALE(mGridOriginScale);
			}
		}
	}
	// 标记指定的塔被选中
	public void showTowerSelect(CharacterTower tower)
	{
		if (tower != null)
		{
			if (mSelectingEffect == null)
			{
				long assignID = tower.getAssignID();
				mEffectManager.createEffectAsyncSafe(EDEffect.TOWER_SELECT.mPath, this, null, true, (GameEffect effect) =>
				{
					if (assignID != tower.getAssignID())
					{
						mEffectManager.destroyEffect(effect);
						return;
					}
					mSelectingEffect = effect;
					effect.setPosition(tower.getPosition());
					effect.play();
				});
			}
			else
			{
				mSelectingEffect.setPosition(tower.getPosition());
			}
		}
		mSelectingEffect?.setActive(tower != null);
	}
	// 显示一个技能的攻击范围
	public void showSkillRange(Vector3 pos, float range)
	{
		mSkillRangeEffect.SetActive(range > 0.0f);
		if (range <= 0.0f)
		{
			return;
		}
		mSkillRangeEffect.transform.localPosition = replaceY(pos, 0.1f);
		mSkillRangeEffect.transform.localScale = new(range * 2.0f, 1.0f, range * 2.0f);
	}
	public Vector3 focusCamera(Vector3 targetPos) { return clampCameraPos(replaceY(targetPos, 0.0f) + getMainCamera().getPosition() - getFocusTerrainPosition()); }
	public void deltaMoveCamera(float deltaX, float deltaZ)
	{
		GameCamera cam = getMainCamera();
		cam.setPositionX(cam.getPosition().x + deltaX);
		cam.setPositionZ(cam.getPosition().z + deltaZ);
		mBattleScene.clampCameraPos();
	}
	public void deltaMoveCamera(float deltaY)
	{
		GameCamera cam = getMainCamera();
		Vector3 pos = cam.getPosition();
		Vector3 dir = cam.getForward() * deltaY;
		Vector3 newPos = pos + dir;
		if(newPos.y < mCameraMinHeight)
		{
			float percent = (mCameraMinHeight - newPos.y) / dir.y;
			newPos.x -= dir.x * percent;
			newPos.z += dir.z * percent;
			newPos.y = mCameraMinHeight;
		}
		else if(newPos.y > mCameraMaxHeight)
		{
			newPos.y = mCameraMaxHeight;
		}
		cam.setPosition(newPos);
		mBattleScene.clampCameraPos();
	}
	public void clampCameraPos() { getMainCamera().setPosition(clampCameraPos(getMainCamera().getPosition())); }
	public Vector3 clampCameraPos(Vector3 cameraPos)
	{
		clamp(ref cameraPos.y, mCameraMinHeight, mCameraMaxHeight);
		if (cameraPos.y >= mCameraMaxHeight || getPointInPlaneSide(mCameraInitPos, getMainCamera().getForward(), cameraPos) <= 0.0f)
		{
			// 如果在摄像机后方就直接拉回原点
			return mCameraInitPos;
		}
		// 让摄像机保持在初始位置的视锥与y轴平面的相交范围内
		Vector3 cameraMinIntersect = intersectRayPlane(new Ray(mCameraInitPos, mCameraMinIntersect - mCameraInitPos), Vector3.up, cameraPos);
		Vector3 cameraMaxIntersect = intersectRayPlane(new Ray(mCameraInitPos, mCameraMaxIntersect - mCameraInitPos), Vector3.up, cameraPos);
		clamp(ref cameraPos.x, cameraMinIntersect.x, cameraMaxIntersect.x);
		clamp(ref cameraPos.z, cameraMinIntersect.z, cameraMaxIntersect.z);
		return cameraPos;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void initGameObject()
	{
		mMonsterRoot = findOrCreateGameObject("MonsterRoot", mRoot);
		mTowerRoot = findOrCreateGameObject("TowerRoot", mRoot);
		mBattlePropRoot = findOrCreateGameObject("BattlePropRoot", mRoot);
		mRemoveableRoot = findOrCreateGameObject("RemoveableRoot", mRoot);
		mGridRoot = findOrCreateGameObject("GridRoot", mRoot);
		mTimeLine = findGameObject("TimeLine", mRoot);
		if (mTimeLine != null)
		{
			mTimeLine.SetActive(false);
		}
		GameObject mainCamera = findOrCreateGameObject("MainCamera", mRoot);
		GameObject zoomInCamera = findGameObject("MainCamera_ZoomIn", mRoot);
		mCameraInitPos = mainCamera.transform.localPosition;
		mCameraMaxHeight = mCameraInitPos.y;
		if (zoomInCamera != null)
		{
			mCameraMinHeight = zoomInCamera.transform.localPosition.y;
			zoomInCamera.SetActive(false);
		}
		else
		{
			mCameraMinHeight = mCameraMaxHeight;
		}
		mHomePoint = findGameObject("P_cunzhangjia", mRoot);
		mTerrain = mMovableObjectManager.createMovableObject(findGameObject("Terrain", mRoot));
		if (1 << mTerrain.getLayer() != MASK_TERRAIN)
		{
			logError("Terrain没设置layer");
		}
	}
	protected void createGridBaseObject(GameObject go, int index)
	{
		go.transform.parent = mGridRoot.transform;
		go.name = "Grid" + index.IToS();
		MovableObject obj = mGridObjectList.add(index, mMovableObjectManager.createMovableObject(go));
		(obj.getCollider() as MeshCollider).convex = true;
		obj.getGameObject().isStatic = true;
		obj.setActive(true);
		obj.setClickDetailCallback(pos => onGridClick(obj, pos));
		mGridRendererList.Add(index, go.GetComponent<Renderer>());
		mGlobalTouchSystem.registeCollider(obj);
		if (mGridObjectList.Count == 1)
		{
			mGridOriginScale = mGridObjectList.get(0).getScale();
		}
	}
	// 点击地面时,关闭所有塔的范围显示
	protected void onTerrainClick()
	{
		CmdGlobalSelectTowerScene.execute(null);
		mTowerDefenceSystem.cmdSelectItemOwned(null);
	}
	protected void onGridClick(MovableObject obj, Vector3 mousePos)
	{
		List<MonsterRoad> roadList = mTowerDefenceSystem.getMonsterRoadList();
		int roadListCount = roadList.Count;
		mBattleScene.getMouseGridIndexAndPoint(mousePos, out _, out Vector3 point);
		BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		int gridIndex = findGridIndex(obj);
		bool canPutTower = true;
		for (int i = 0; i < roadListCount; ++i)
		{
			canPutTower &= checkCanPutTower(i, gridIndex);
		}
		if (mDragOnlyGrid >= 0 && gridIndex != mDragOnlyGrid)
		{
			canPutTower = false;
		}
		// Rogue模式
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			EDTower towerData = mUIClientPackRogue.getReadyToSetupTower()?.getTowerData();
			if (towerData == null)
			{
				return;
			}
			do
			{
				if (!canPutTower)
				{
					break;
				}
				int buildCost = mExcelTower.getRogueNextLevelCost(towerData, 0);
				if (mTowerDefenceSystem.getGoldCoinRogue() < buildCost)
				{
					tip("道具不足，需要{0}", buildCost.IToS());
					break;
				}
				CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() - buildCost);
				var tower = CmdGlobalCreateTower.execute(towerData, point);
				tower.getTowerData().addUseCoin(buildCost);
				CmdGlobalPutTowerRogue.execute(tower, gridIndex, 0);
			} while (false);
			CmdGlobalSelectItemOwnedRogue.execute(null);
		}
	}
	protected int findGridIndex(IMouseEventCollect obj)
	{
		int count = mGridObjectList.Count;
		for (int i = 0; i < count; ++i)
		{
			if (mGridObjectList.get(i) == obj)
			{
				return i;
			}
		}
		return -1;
	}
	// 显示怪物的寻路路线
	protected void setPathPoints(myLineRenderer lineRenderer, List<int> path)
	{
		lineRenderer.setActive(!path.isEmpty());
		// 显示新的路径
		if (path.isEmpty())
		{
			return;
		}
		int curCount = path.Count;
		Span<Vector3> points = stackalloc Vector3[curCount * 3 - 2];
		for (int i = 0; i < curCount; ++i)
		{
			points[i * 3] = getGridPosition(path[i]);
		}
		for (int i = 1; i < curCount; ++i)
		{
			points[i * 3 - 2] = lerp(points[i * 3 - 3], points[i * 3], 0.01f);
			points[i * 3 - 1] = lerp(points[i * 3 - 3], points[i * 3], 0.99f);
		}
		lineRenderer.setPointList(points);
	}
	protected GameEffect createNewPathEffect(int index)
	{
		List<GameEffect> pathList = mMovePathEffect[index];
		GameEffect effect = pathList.find(item => !item.isActive());
		if (effect == null)
		{
			effect = pathList.add(mEffectManager.createEffect(EDEffect.BATTLE_MOVE_PATH.mPath, null, mGridRoot, false, false));
			effect.enableMoveInfo();
			effect.getCOMMoveInfo().setActive(true);
		}
		effect.setActive(true);
		return effect;
	}
	protected void startPathEffect(int index, List<Vector3> points)
	{
        createNewPathEffect(index).MOVE_CURVE_EX(KEY_CURVE.ZERO_ONE, points, BATTLE_PATH_EFFECT_TIME,
		(com, isBreak) =>
		{
			if (com.getOwner() is not GameEffect effect || !effect.isActive())
			{
				return;
			}
			if (effect.hasLastPosition())
			{
				effect.lookAt(effect.getPosition() - effect.getLastPosition());
			}
		},
		(com, isBreak) =>
		{
			if (com.getOwner() is not GameEffect effect || !effect.isActive())
			{
				return;
			}
			effect.setActive(false);
		});
	}
}