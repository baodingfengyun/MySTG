using UnityEngine;
using static GameUtilityHotFix;

// ===== 怪物实体：一只怪 = 一组功能组件 + 一份数据表 + 一堆状态(Buff/动作) =====
// 【组件化】怪物自身几乎不写逻辑，能力都拆到 6 个 COM 组件：
//   COMMonsterMovement(寻路移动) / COMMonsterLifeTime(存活计时) / COMMonsterAvatar(模型动画) /
//   COMMonsterSkill(技能) / COMMonsterTalk(台词) / COMCharacterBleeding(出血)。
//   基类还自动挂了 HP/Buff/状态机等通用组件。addComponent(out x, true) 的 true=参与对象池回收。
// 【组件顺序有讲究】initComponents 里技能组件必须在模型组件之前加——销毁时能先清技能特效再销毁模型。
// 【initData 链】填入表数据 → 初始化移动/速度 → 拷贝六抗+闪避+HP → 异步加载模型 →
//   注册 ActionWalk 动画层 → 挂默认 Buff → 学技能 → 把移动回调接到出血组件(走路留血迹)。
// 【开战】notifyFightStart 触发被动技能(如出生自爆前的蓄力)。
// 【Avatar 特化】构造函数里 addDontAutoCreate<COMCharacterAvatar> 禁止基类自动建 Avatar，
//   改用继承的 COMMonsterAvatar——因为怪物动画接口(MONSTER_ANIMATION)和塔/玩家不同。
public class CharacterMonster : CharacterGame
{
	protected CharacterMonsterData mMonsterData = new();	// 怪物数据
	protected COMCharacterBleeding mComBleeding;			// 出血组件
	protected COMMonsterMovement mComMovement;				// 移动组件
	protected COMMonsterLifeTime mComLifeTime;              // 生存时间组件
	protected COMMonsterAvatar mComAvatar;					// 显示组件
	protected COMMonsterSkill mComSkill;					// 技能组件
	protected COMMonsterTalk mComTalk;						// 说话组件
	public CharacterMonster()
	{
		mGameData = mMonsterData;
		// 不需要基类自动添加Avatar组件,手动添加一个继承后的Avatar组件
		addDontAutoCreate<COMCharacterAvatar>();
	}
	public void initData(EDMonster monsterData)
	{
		mMonsterData.mTableData = monsterData;
		setName(monsterData.mName);
		mComMovement.initData();
		mComMovement.setSpeed(monsterData.mSpeed);
		mMonsterData.mEvasion = monsterData.mEvasion;
		mMonsterData.mDefence = monsterData.mDefence;
		mMonsterData.mAntiFireElement = monsterData.mAntiFire;
		mMonsterData.mAntiIceElement = monsterData.mAntiIce;
		mMonsterData.mAntiDarkElement = monsterData.mAntiDark;
		mMonsterData.mAntiLightElement = monsterData.mAntiLight;
		mMonsterData.mAntiPoisonElement = monsterData.mAntiPoison;
		mMonsterData.mAntiLightningElement = monsterData.mAntiLightning;
		mMonsterData.mMaxHP = monsterData.mHP;
		mMonsterData.mHP = mMonsterData.mMaxHP;
		mComAvatar.loadModelAsync(monsterData.mPrefab);
		mCOMAnimation.addLayer<ActionWalk, StateGroupAction>();

		// 添加初始buff
		foreach (int buffID in monsterData.mDefaultBuff)
		{
			if (buffID > 0)
			{
				characterAddBuff(buffID, this, null);
			}
		}

		foreach (int skillID in monsterData.mSkill)
		{
			if (skillID > 0)
			{
				mComSkill.addSkill(skillID);
			}
		}

		// 把移动回调接到出血组件：怪物每走一段就由出血组件决定是否留血迹
		mComMovement.setMovingCallback(mComBleeding.getListenFunction());

#if UNITY_EDITOR
		getOrAddUnityComponent<MonsterDebug>().setMonster(this);
#endif
	}
	public void notifyFightStart()
	{
		mComSkill.firePassiveSkill();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsterData.resetProperty();
		mComBleeding = null;
		mComMovement = null;
		mComLifeTime = null;
		mComAvatar = null;
		mComSkill = null;
		mComTalk = null;
	}
	public override Collider getCollider(bool addIfNotExist = false) { return mComAvatar?.getCollider(); }
	public CharacterMonsterData getMonsterData() { return mMonsterData; }
	public COMCharacterBleeding getComBleeding() { return mComBleeding; }
	public COMMonsterMovement getComMovement() { return mComMovement; }
	public COMMonsterLifeTime getComLifeTime() { return mComLifeTime; }
	public COMMonsterAvatar getComAvatar() { return mComAvatar; }
	public COMMonsterSkill getComSkill() { return mComSkill; }
	public COMMonsterTalk getComTalk() { return mComTalk; }
	public override Transform getFootPoint() { return mComAvatar?.getFootPoint(); }
	public override Transform getBodyPoint() { return mComAvatar?.getBodyPoint(); }
	public override Transform getHeadPoint() { return mComAvatar?.getHeadPoint(); }
	public override int getHP() { return mMonsterData.mHP; }
	public override int getMaxHP() { return mMonsterData.mMaxHP; }
	public override int getTableID() { return mMonsterData.mTableData.mID; }
	//------------------------------------------------------------------------------------------------------------------------------
	// 初始化怪物的功能组件
	protected override void initComponents()
	{
		// 角色基础组件
		base.initComponents();
		// 怪物特有组件
		addComponent(out mComBleeding, true);
		addComponent(out mComMovement, true);
		addComponent(out mComLifeTime, true);
		addComponent(out mComSkill, true);		// 技能组件需要在模型组件之前添加,这样可以先销毁技能,以及销毁挂在角色上的技能相关特效
		addComponent(out mComAvatar, true);
		addComponent(out mComTalk, true);
		mAvatar = mComAvatar;
	}
	// 打印一个怪物的基本信息
    public override string ToString()
    {
		return "Monster id:" + getTableID() + ", guid:" + mGUID + ", name:" + mName + ", hp:" + getHP();
    }
}