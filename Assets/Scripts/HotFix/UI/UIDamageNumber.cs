using Obfuz;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MathUtility;
using static UnityUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDamageNumber.prefab
// ===== 伤害数字显示：飘字(普通/DEBUFF/MISS/暴击)的对象池管理器 =====
// 【三类数字】init 里注册三种：DamageNumberNormal(普通伤害,pg01)、DamageNumberDebuff(毒/烧等持续伤害,bj03)、
//   DamageNumberMiss(闪避,bj03)。每张表用 HP_DELTA 类型索引到对应的池 DamageNumberInfo<T>。
// 【显示流程 showNumber】按 HP_DELTA 取池 → newItem 取一个数字对象 → 设数值/暴击样式 →
//   MOVE_PATH_EX 沿配置路径飘动 + SCALE_PATH 缩放；移动结束回调 onNumberMoveDone 回收进池。
// 【上限保护】最多 100 个同时显示，超出时强制回收最早的一批(MOVE/SCALE 停止动画再 unuseItem)。
// 【对象池】每种数字用自己的 ClassPool(mNumberPool)，模板来自 prefab 的三个节点，避免频繁实例化。
// 【性能】mNeedUpdate=false——本界面不逐帧 tick，飘字动画全交给关键帧组件(ComponentKeyFrame)。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIDamageNumber : LayoutScript
{
    protected myUGUIObject mNormal;
    protected myUGUIObject mDebuff;
    protected myUGUIObject mMiss;
    // auto generate member end
    protected Dictionary<HP_DELTA, DamageNumberInfoBase> mHPDeltaTypeList = new();
	protected Dictionary<myUGUIObject, DamageNumber> mNumberSearchList = new();
	protected Dictionary<Type, DamageNumberInfoBase> mNumberInfoList = new();
	protected List<DamageNumber> mAllDamageNumberList = new();						// 所有类型的正在显示的伤害数字列表
	public UIDamageNumber()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void setLayout(GameLayout layout)
	{
		base.setLayout(layout);
		mLayout.setDefaultUpdateWindow(false);
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mNormal, "Normal");
		newObject(out mDebuff, "Debuff");
		newObject(out mMiss, "Miss");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		registeDamageNumber<DamageNumberNormal>(mNormal, "pg01", HP_DELTA.NORMAL_DAMAGE);
		registeDamageNumber<DamageNumberDebuff>(mDebuff, "bj03", HP_DELTA.DEBUFF);
		registeDamageNumber<DamageNumberMiss>(mMiss, "bj03", HP_DELTA.MISS);
	}
	// 显示一个飘字：position 是世界坐标，deltaType 决定用哪个数字池，critical 控制暴击样式(MISS 不显示暴击)
	public void showNumber(Vector3 position, int number, HP_DELTA deltaType, bool critical)
	{
		if (!mHPDeltaTypeList.TryGetValue(deltaType, out DamageNumberInfoBase numberInfo))
		{
			log("伤害数字未注册:" + deltaType);
			return;
		}

		// 超过最大数量就移除一开始的数字
		int maxNumberCount = 100;
		if (maxNumberCount > 0 && mAllDamageNumberList.Count >= maxNumberCount)
		{
			int removeCount = mAllDamageNumberList.Count - maxNumberCount + 1;
			for (int i = 0; i < removeCount; ++i)
			{
				DamageNumber thisNumber = mAllDamageNumberList[i];
                thisNumber.getRoot().MOVE();
                thisNumber.getRoot().SCALE();
				mNumberInfoList.get(thisNumber.GetType())?.unuseItem(thisNumber);
				mNumberSearchList.Remove(thisNumber.getRoot());
			}
			mAllDamageNumberList.RemoveRange(0, removeCount);
		}

		// 根据类型创建对应的伤害数字,并且加入到查询列表中
		DamageNumber numberItem = numberInfo.newItem();
		numberItem.setCriticalHitEnabled(deltaType != HP_DELTA.MISS && critical);
		myUGUIObject root = numberItem.getRoot();
		mNumberSearchList.add(root, numberItem).setNumber(number);
		// 数字的移动
		Vector3 randomOffset = new(randomFloat(-40.0f, 40.0f), randomFloat(-15.0f, 15.0f));
        root.MOVE_PATH_EX(numberInfo.getTranslatePath(), position + randomOffset, onNumberMoveDone);
        root.SCALE_PATH(numberInfo.getScalePath(), new(0.3f, 0.3f, 0.3f));
		mAllDamageNumberList.add(numberItem);
	}
	// 回收所有数字窗口
	public void unuseAllNumber()
	{
		foreach (DamageNumberInfoBase item in mNumberInfoList.Values)
		{
			item.unuseAll();
		}
		mNumberSearchList.Clear();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onNumberMoveDone(ComponentKeyFrame com, bool isBreak)
	{
		if (isBreak)
		{
			return;
		}
		if (!mNumberSearchList.Remove(com.getOwner() as myUGUIObject, out DamageNumber number))
		{
			logError("伤害数字移动完成后回收失败");
			return;
		}
		mNumberInfoList.get(number.GetType())?.unuseItem(number);
		mAllDamageNumberList.Remove(number);
	}
	protected void registeDamageNumber<T>(myUGUIObject template, string pathName, HP_DELTA deltaType) where T : DamageNumber
	{
		DamageNumberInfo<T> info = new();
		info.mNumberPool = new(this);
		info.mNumberPool.assignTemplate(template);
        info.mNumberPool.init();
		info.mNumberPath = pathName;
		info.mDeltaType = deltaType;
		mNumberInfoList.Add(typeof(T), info);
		mHPDeltaTypeList.Add(deltaType, info);
	}
}