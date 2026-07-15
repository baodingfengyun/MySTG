using static GBR;
using static GameUtilityHotFix;

// 选择的卡牌生效,Rogue模式
public class CmdGlobalOwnedPropAddRogue
{
	public static void execute(EDTowerTalent data, bool addTalentDict = true)
	{
		if (data == null)
		{
			return;
		}
		if(addTalentDict)
		{
			mTowerDefenceSystem.getBattleModeRogue().addTaltent(data);
		}
		Character globalCharacter = mTowerDefenceSystem.getBattleModeInstance().getGlobalCharacter();
		foreach (int id in data.mBuff)
		{
			characterAddBuff(id, globalCharacter, null);
		}
	}
}