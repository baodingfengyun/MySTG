// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// MapConfig表格
public class EDMapConfig : ExcelDataT<EDMapConfig>
{
	public string mName;							// 地图名称
	public string mSceneName;						// 地图场景路径
	public int mWidth;								// 横向格子数量,需要与格子点中的数量对应
	public int mHeight;								// 纵向格子数量,需要与格子点中的数量对应
	public GRID_TYPE mGridDirection;				// 格子的行走方向数量
	public List<int> mPoints = new();				// 所有的格子点
	public List<int> mSpawnPoint = new();			// 起点格子下标列表
	public List<int> mTargetPoint = new();			// 终点格子下标
	public List<string> mGridProp = new();			// 地图格子机关
	public List<string> mStageProp = new();			// 地图物体机关
	public List<string> mRemoveables = new();		// 可移除物件的位置Excel ObstacleConfig，只能在格子为2(可行走)的位置生效
	public int mTheme;								// 生成地块的主题
	public Vector3 mGridRootPos;					// 地图格中心坐标
	public Vector3 mCameraInitPos;					// 场景摄像机初始位置
	public float mCameraBattlePos;					// 战斗开始摄像机缩进向量
	public Vector3 mCameraScalePos;					// 场景摄像机拉近位置
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mSceneName);
		result = result && reader.read(out mWidth);
		result = result && reader.read(out mHeight);
		result = result && reader.readEnumByte(out mGridDirection);
		result = result && reader.readList(mPoints);
		result = result && reader.readList(mSpawnPoint);
		result = result && reader.readList(mTargetPoint);
		result = result && reader.readList(mGridProp);
		result = result && reader.readList(mStageProp);
		result = result && reader.readList(mRemoveables);
		result = result && reader.read(out mTheme);
		result = result && reader.read(out mGridRootPos);
		result = result && reader.read(out mCameraInitPos);
		result = result && reader.read(out mCameraBattlePos);
		result = result && reader.read(out mCameraScalePos);
		return result;
	}
}
// auto generate end