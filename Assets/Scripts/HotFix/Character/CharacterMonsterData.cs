
// 怪物运行时数据：表配置(只读) + 战斗中会变的量(HP/击杀者/隐身/飞行标记)
// 【为什么单列 mIsInvisible / mFlyable 两个缓存】隐身靠"身上有没有隐身 Buff"判断太慢且不准
//   （可能有多个来源），飞行同理要查状态组；所以各用一个字段缓存，避免每帧查状态机。
// 【mKillerGUID】记录是谁打死了它——死亡结算发金币/经验/触发"击杀"事件时要归因。
public class CharacterMonsterData : CharacterGameData
{
	public EDMonster mTableData;    // 怪物的表格数据（配置）
	public long mKillerGUID;		// 击杀此怪物的角色ID
	public int mMaxHP;				// 怪物的最大血量
	public int mHP;                 // 怪物的当前血量
	public byte mIsInvisible;       // 是否为隐身怪,不能单独通过有没有指定buff来判断是否为隐身怪,所以只能添加一个变量记录
	public bool mFlyable;           // 是否可飞行,只是用于加快判断效率,避免频繁查找状态
	// 重置属性和其它数据
	public override void resetProperty()
	{
		base.resetProperty();
		mTableData = null;
		mKillerGUID = 0;
		mMaxHP = 0;
		mHP = 0;
		mFlyable = false;
		mIsInvisible = 0;
	}
}