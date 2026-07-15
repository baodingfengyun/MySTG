
// 怪物数据
public class CharacterMonsterData : CharacterGameData
{
	public EDMonster mTableData;    // 怪物的表格数据
	public long mKillerGUID;		// 击杀此怪物的角色ID
	public int mMaxHP;				// 怪物的最大血量
	public int mHP;                 // 怪物的当前血量
	public byte mIsInvisible;       // 是否为隐身怪,不能单独通过有没有指定buff来判断是否为隐身怪,所以只能添加一个变量记录
	public bool mFlyable;           // 是否可飞行,只是用于加快判断效率,避免频繁查找状态
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