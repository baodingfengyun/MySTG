
// 石墩（继承自防御塔）：一个"纯占位"的特殊塔,本身不攻击,用来给玩家拖拽道具/英雄上去
// 【为什么要有专属类】绝大多数塔用基类 CharacterTower 即可,石墩因为"能否被操作"规则特殊才单独建类
// 【canOperate】 overridden 后恒返回 true —— 即使石墩上已经放了道具,仍可被玩家继续操作
//   （原注释写"放了道具后不能再拖拽"与代码不符,实际返回 true 表示始终可操作）
// 【注册】通过 TowerRegister.registerTower<CharacterTowerStone>(TOWER_TYPE.SHI_DUN) 反射创建
public class CharacterTowerStone : CharacterTower
{
	public override bool canOperate()
	{
		// 恒为 true：石墩始终允许被操作（拖拽/放置）
		return true;
	}
}