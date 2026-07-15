
// 用于挂在start场景的节点上,作为程序入口
public class GameEntry : GameEntryBase
{
	public override void Awake()
	{
		base.Awake();
		Game.startGame();
	}
}