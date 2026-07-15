using static FrameBaseHotFix;

public class CmdGlobalDestroyMonster
{
	public static void execute(CharacterMonster monster)
	{
		using var a = new ClassScope<EventMonsterDestroy>(out var eventParam);
		eventParam.mMonster = monster;
		mEventSystem.pushEvent(eventParam, monster.getGUID());
		mCharacterManager.destroyCharacter(monster);
	}
}