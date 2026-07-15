using UnityEngine;
using static FrameBaseHotFix;
using static TowerRegister;

// 创建一个防御塔对象
public class CmdGlobalCreateTower
{
	public static CharacterTower execute(EDTower towerData, Vector3 pos)
	{
		var tower = mCharacterManager.createCharacter(towerData.mName, getTowerType(towerData.mType)) as CharacterTower;
		tower.setPosition(pos);
		tower.initData(towerData);
		return tower;
	}
}