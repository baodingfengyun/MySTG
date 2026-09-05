using static FrameBaseHotFix;

// 销毁一个怪物
public class CmdGlobalDestroyMonster
{
	public static void execute(CharacterMonster monster)
	{
		// 销毁怪物事件
		using var a = new ClassScope<EventMonsterDestroy>(out var eventParam);
		eventParam.mMonster = monster;
		mEventSystem.pushEvent(eventParam, monster.getGUID());
		mCharacterManager.destroyCharacter(monster);
	}
}