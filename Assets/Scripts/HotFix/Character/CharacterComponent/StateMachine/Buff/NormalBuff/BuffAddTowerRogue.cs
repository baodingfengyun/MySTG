using static FrameUtility;
using static StringUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffAddTowerRogueParam : CharacterBuffParamT<BuffAddTowerRogueParam>
{
	public TOWER_TYPE mTower;				// 塔
	public override void registeAllParam()
	{
		registeParam((param) => { mTower = (TOWER_TYPE)param.SToI(); });
	}
	protected override void copyInternal(BuffAddTowerRogueParam other)
	{
		mTower = other.mTower;
	}
	public override void check()
	{
		checkEnum(mTower);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = 0;
	}
}

// 将防御塔带入肉鸽战斗
public class BuffAddTowerRogue : CharacterBuffT<BuffAddTowerRogueParam>
{
	public override void enter()
	{
		base.enter();
		CmdGlobalAddRogueTower.execute(mCustomParam.mTower);
	}
}