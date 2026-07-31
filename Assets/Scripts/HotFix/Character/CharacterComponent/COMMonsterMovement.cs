using System.Collections.Generic;
using UnityEngine;
using static MathUtility;
using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 怪物移动逻辑,沿着指定路线移动
public class COMMonsterMovement : GameComponent
{
	protected List<int> mRoadPointList = new();		// 路点列表,值是格子下标,每个怪物会拷贝一份路线
	protected CharacterMonster mMonster;			// 所属怪物
	protected FloatCallback mMovingCallback;		// 移动的回调
	protected Vector3 mTargetPosition;				// 当前移动的目标世界坐标
	protected MONSTER_GRID_OFFSET mRandomGridOffset;// 怪物初始化后偏移方向
	protected float mSpeed;							// 移动速度,因为可能有加速或者减速,所以不能直接使用表格的字段
	protected int mTargetPointIndex;				// mRoadPointList的下标
	protected int mGridIndex;						// 当前所处的格子下标
	protected bool mMoveFinish;						// 是否已经移动结束了,为了避免在更新怪物时销毁怪物,所以只是记录一个变量
	protected bool mConfusion;						// 是否为混乱状态,混乱时会反方向移动.为了提高效率,不会每次移动都判断是否有混乱状态,所以会记录一个变量
	protected const float TURN_SPEED = 3.0f;		// 转向的速度,弧度制
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mMonster = mComponentOwner as CharacterMonster;
	}
	public void initData()
	{
		if (mMonster.getMonsterData().mTableData.mStrength != MONSTER_STRENGTH.BOSS)
		{
			mRandomGridOffset = (MONSTER_GRID_OFFSET)randomInt(0, (int)MONSTER_GRID_OFFSET.MAX - 1);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRoadPointList.Clear();
		mMonster = null;
		mMovingCallback = null;
		mTargetPosition = Vector3.zero;
		mRandomGridOffset = MONSTER_GRID_OFFSET.NONE;
		mSpeed = 0.0f;
		mTargetPointIndex = 0;
		mGridIndex = -1;
		mMoveFinish = false;
		mConfusion = false;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mRoadPointList == null ||
			mRoadPointList.Count == 0 ||
			mTargetPointIndex < 0 ||
			mTargetPointIndex >= mRoadPointList.Count ||
			mMonster.getMonsterData().mHP <= 0 ||
			mMonster.hasStateGroup<StateGroupNotAllowMove>() ||
			mSpeed <= 0.0f)
		{
			return;
		}
		float delta = elapsedTime * mSpeed;
		if (!mConfusion)
		{
			moveForward(delta);
		}
		else
		{
			moveBackward(delta);
		}
		mMovingCallback?.Invoke(delta);
		float curYaw = getVectorYaw(mMonster.getForward());
		float targetDirection = getVectorYaw(mTargetPosition - mMonster.getPosition());
		perfectRotationDeltaRadian(ref curYaw, ref targetDirection);
		curYaw = lerp(curYaw, targetDirection, 0.1f, 0.1f);
		// 始终朝向移动方向
		mMonster.lookAt(getDirectionFromRadianYawPitch(curYaw, 0.0f));
	}
	public void moveForward(float moveDelta, bool triggerEvent = true)
	{
		int lastGridIndex = mGridIndex;
		Vector3 curPos = mMonster.getPosition();
		// 为了避免一帧移动跨越了多个格子而导致的复杂情况,所以使用循环递减的方式,每次循环只处理一个格子的情况
		while (true)
		{
			// 剩余距离还没有到下一个点,则直接设置位置
			if ((curPos - mTargetPosition).lengthGreater(moveDelta))
			{
				curPos += (mTargetPosition - curPos).setLength(moveDelta);
				break;
			}
			// 已经移动到最后一个点,移动结束
			if (mTargetPointIndex >= mRoadPointList.Count - 1)
			{
				curPos = mTargetPosition;
				mMoveFinish = true;
				break;
			}
			// 移动的距离超过了下一个点,则减去到下一个点的距离,继续计算
			moveDelta -= (mTargetPosition - curPos).getLength();
			curPos = mTargetPosition;
			setTargetPointIndex(mTargetPointIndex + 1);
		}
		mMonster.setPosition(curPos);
		mGridIndex = mBattleScene.worldPointToGridIndex(curPos, mGridIndex);
		// 触发格子改变的事件,如果一帧中跨过了多个格子,则只处理最后一个格子的改变,因为中间可能是瞬移过去的,瞬移过去应该不会触发中间格子会合理一些
		if (mGridIndex != lastGridIndex && triggerEvent)
		{
			using var a = new ClassScope<EventMonsterGridChange>(out var param);
			param.mMonster = mMonster;
			mEventSystem.pushEvent(param, mMonster.getGUID());
		}
	}
	public void moveBackward(float moveDelta, bool triggerEvent = true)
	{
		int lastGridIndex = mGridIndex;
		Vector3 curPos = mMonster.getPosition();
		// 为了避免一帧移动跨越了多个格子而导致的复杂情况,所以使用循环递减的方式,每次循环只处理一个格子的情况
		while (true)
		{
			// 剩余距离还没有到下一个点,则直接设置位置
			if ((curPos - mTargetPosition).lengthGreater(moveDelta))
			{
				curPos += (mTargetPosition - curPos).setLength(moveDelta);
				break;
			}
			// 已经移动到起点,不再继续移动
			if (mTargetPointIndex <= 0)
			{
				curPos = mTargetPosition;
				break;
			}
			// 移动的距离超过了下一个点,则减去到下一个点的距离,继续计算
			moveDelta -= (mTargetPosition - curPos).getLength();
			curPos = mTargetPosition;
			// 切换到路线的前一个目标点
			setTargetPointIndex(mTargetPointIndex - 1);
		}
		mMonster.setPosition(curPos);
		mGridIndex = mBattleScene.worldPointToGridIndex(curPos, mGridIndex);
		// 触发格子改变的事件,如果一帧中跨过了多个格子,则只处理最后一个格子的改变,因为中间可能是瞬移过去的,瞬移过去应该不会触发中间格子会合理一些
		if (mGridIndex != lastGridIndex && triggerEvent)
		{
			using var a = new ClassScope<EventMonsterGridChange>(out var param);
			param.mMonster = mMonster;
			mEventSystem.pushEvent(param, mMonster.getGUID());
		}
	}
	public void setSpeed(float speed) { mSpeed = speed; }
	public void setMovingCallback(FloatCallback callback) { mMovingCallback = callback; }
	public void setConfusion(bool confusion)
	{
		if (mConfusion == confusion)
		{
			return;
		}
		mConfusion = confusion;
		// 设置为混乱时,需要改变当前行进方向,以及当前的目标点
		generateNextRoadIndex(mMonster.getPosition(), out int index, !mConfusion);
		// 如果无法计算出当前的目标点,则可能怪物当前不在路线上,可能是被击退了,无法计算出当前该往哪个点走
		// 召唤出来的怪物,移动起点不是关卡起点,所以也会出现在召唤出来的地方不动的情况
		// 只能保持当前的目标点不变
		if (index < 0)
		{
			return;
		}
		setTargetPointIndex(index);
	}
	public List<int> getRoadPointList() { return mRoadPointList; }
	public float getSpeed() { return mSpeed; }
	public bool isMoveFinish() { return mMoveFinish; }
	public Vector3 getTargetPosition() { return mTargetPosition; }
	public int getTargetPointIndex() { return mTargetPointIndex; }
	public int getGridIndex() { return mGridIndex; }
	// 获得到终点的距离
	public float getDistanceToEnd()
	{
		return (mMonster.getPosition() - mTargetPosition).getLength() + (mRoadPointList.Count - 1 - mTargetPointIndex) * GRID_SIZE;
	}
	public void startMove()
	{
		if (mRoadPointList.Count == 0)
		{
			return;
		}
		// 先设置到第一个点,获取坐标
		setTargetPointIndex(0);
		mMonster.setPosition(mTargetPosition);
		// 初始化位置时手动同步一下模型的位置,避免一开始怪物的位置与模型位置不一致导致的一些奇怪的错误
		mMonster.getAvatar().syncTransform();
		mGridIndex = mBattleScene.worldPointToGridIndex(mTargetPosition, mGridIndex);
		// 添加移动行为状态
		mMonster.getStateMachine().addState<ActionWalk>();
		// 再将目标修改为第二个点
		setTargetPointIndex(1);
		// 初始时朝向前进的方向
		mMonster.lookAtPoint(mTargetPosition);
	}
	public void setTargetPointIndex(int index)
	{
		mTargetPointIndex = index;
		if (mTargetPointIndex >= 0 && mTargetPointIndex < mRoadPointList.Count)
		{
			mTargetPosition = mBattleScene.getGridPosition(mRoadPointList[mTargetPointIndex]);
			Vector3 direction = Vector3.zero;
			if (mTargetPointIndex > 0)
			{
				direction = mTargetPosition - mBattleScene.getGridPosition(mRoadPointList[mTargetPointIndex - 1]);
			}
			// 随机偏移
			if (mRandomGridOffset == MONSTER_GRID_OFFSET.LEFT)
			{
				mTargetPosition += direction.rotate(HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
			}
			else if(mRandomGridOffset == MONSTER_GRID_OFFSET.RIGHT)
			{
				mTargetPosition += direction.rotate(-HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
			}
			if (mMonster.getMonsterData().mFlyable)
			{
				mTargetPosition += new Vector3(0.0f, FLY_MONSTER_HEIGHT, 0.0f);
			}
		}
	}
	// 检查当前是否有可走的路线
	public bool checkPath(int extraBlockIndex)
	{
		// 有怪物不在有效的格子中时,不允许放置塔,否则放置后无法计算怪物路线
		int curGridIndex = mBattleScene.worldPointToGridIndex(mMonster.getPosition(), mGridIndex);
		if (curGridIndex < 0)
		{
			return false;
		}
		if (mMonster.getMonsterData().mFlyable)
		{
			return mTowerDefenceSystem.generateFlyRoadPathCustom(curGridIndex, null, extraBlockIndex);
		}
		else
		{
			return mTowerDefenceSystem.generateWalkRoadPathCustom(curGridIndex, null, extraBlockIndex);
		}
	}
	// 地图上塔的位置有改变,重新计算怪物的路线
	public void regenerateRoadList()
	{
		int curGridIndex = mBattleScene.worldPointToGridIndex(mMonster.getPosition(), mGridIndex);
		if (curGridIndex < 0)
		{
			// 此处的错误可能是怪物没有处于任何格子中,比如击退到地图外以后,刚好开始重新计算路线时出现的.属于正常逻辑,不需要报错
			logWarning("怪物当前所属格子下标错误:" + curGridIndex + ", pos:" + mMonster.getPosition());
		}
		bool success;
		if (mMonster.getMonsterData().mFlyable)
		{
			success = mTowerDefenceSystem.generateFlyRoadPathCustom(curGridIndex, mRoadPointList, -1);
		}
		else
		{
			success = mTowerDefenceSystem.generateWalkRoadPathCustom(curGridIndex, mRoadPointList, -1);
		}
		if (!success)
		{
			logError("怪物的路线刷新失败,当前格子下标:" + curGridIndex);
			mRoadPointList.Clear();
			return;
		}
		checkRoadPointBetween(0);
	}
	// 检查当前位于哪两个点之间
	public void checkRoadPointBetween(int defaultIndex = -1)
	{
		generateNextRoadIndex(mMonster.getPosition(), out int index, !mConfusion);
		// 没有在路线上
		if (index < 0)
		{
			setTargetPointIndex(defaultIndex >= 0 ? defaultIndex : mTargetPointIndex);
			return;
		}
		// 如果当前位于第0个点到第1个点的连线上,则直接前往第1个点即可
		setTargetPointIndex(index);
	}
	// 寻路路线重新计算后,需要刷新当前记录的目标格子下标
	public void setRoadPointList(List<int> newIndexList)
	{
		if (mRoadPointList.Count > 0)
		{
			logError("不能直接替换新的路线");
			return;
		}
		// 设置路线
		mRoadPointList.AddRange(newIndexList);
	}
	// 根据pos计算出位于路线的哪两个点之间,返回的是mRoadPointList中的下标
	public void generateNextRoadIndex(Vector3 pos, out int index, bool forward)
	{
		// 忽略Y轴
		pos = pos.resetY();
		if (forward)
		{
			int count = mRoadPointList.Count - 1;
			for (int i = 0; i < count; ++i)
			{
				// 因为会有3条不同的路径,所以需要三条都检查
				Vector3 pos0 = mBattleScene.getGridPosition(mRoadPointList[i]).resetY();
				Vector3 pos1 = mBattleScene.getGridPosition(mRoadPointList[i + 1]).resetY();
				Vector3 offset = Vector3.zero;
				if (mRandomGridOffset == MONSTER_GRID_OFFSET.LEFT)
				{
					offset = (pos1 - pos0).rotate(HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
				}
				else if (mRandomGridOffset == MONSTER_GRID_OFFSET.RIGHT)
				{
					offset = (pos1 - pos0).rotate(-HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
				}
				if (isInLine(pos, pos0 + offset, pos1 + offset))
				{
					index = i + 1;
					return;
				}
			}
		}
		else
		{
			int count = mRoadPointList.Count;
			for (int i = count - 1; i > 0; --i)
			{
				Vector3 pos0 = mBattleScene.getGridPosition(mRoadPointList[i]).resetY();
				Vector3 pos1 = mBattleScene.getGridPosition(mRoadPointList[i - 1]).resetY();
				Vector3 offset = Vector3.zero;
				if (mRandomGridOffset == MONSTER_GRID_OFFSET.LEFT)
				{
					offset = (pos0 - pos1).rotate(HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
				}
				else if (mRandomGridOffset == MONSTER_GRID_OFFSET.RIGHT)
				{
					offset = (pos0 - pos1).rotate(-HALF_PI_RADIAN).setLength(HEX_MONSTER_MOVE_OFFSET);
				}
				if (isInLine(pos, pos0 + offset, pos1 + offset))
				{
					index = i - 1;
					return;
				}
			}
		}
		index = -1;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static bool isInLine(Vector3 pos, Vector3 pos0, Vector3 pos1)
	{
		return pos.isEqual(pos0) ||
			   pos.isEqual(pos1) || 
			   isPointInSection(new(pos.x, pos.z), new(new(pos0.x, pos0.z), new(pos1.x, pos1.z)));
	}
}