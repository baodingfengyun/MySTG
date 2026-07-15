using static FrameBaseHotFix;
using static GDR;
using static GBR;

// 创建一个传送门对象
public class CmdGlobalCreateOrSetPortal
{
	public static CharacterPortal execute(EDMapPortal tableData, int gridIndex, bool isEntry)
	{
		CharacterPortal character = mTowerDefenceSystem.getPortalAtGrid(gridIndex);
		if(character == null)
		{
			character = mCharacterManager.createCharacter<CharacterPortal>("Portal");
			character.setModel(mExcelEffect.query(MAP_PORTAL_EFFECT_ID).mPath);
			character.setGridIndexAndPosition(gridIndex);
			mTowerDefenceSystem.addPortal(character);
			mTowerDefenceSystem.setPortalGridIndex(character, gridIndex);
		}
		if (isEntry)
		{
			character.initData(tableData);
		}
		return character;
	}
}