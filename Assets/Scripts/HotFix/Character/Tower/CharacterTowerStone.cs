
// 石墩（继承自防御塔）
public class CharacterTowerStone : CharacterTower
{
	public override bool canOperate()
	{
		// 石墩上放了道具后就不能再拖拽移动位置了
		return true;
	}
}