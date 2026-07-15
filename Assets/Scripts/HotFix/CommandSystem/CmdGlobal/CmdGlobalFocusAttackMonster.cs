using static GBR;
using static GDR;
using static GameUtilityHotFix;

// 集火怪物的buff
public class CmdGlobalFocusAttackMonster
{
	public static void execute(CharacterMonster monster)
	{
		if(monster.getMonsterData().mHP <= 0)
		{
			return;
		}
		foreach (CharacterMonster item in mTowerDefenceSystem.getMonsterMainList())
		{
			item.getStateMachine().removeFirstState<BuffFocusAttackMonster>(false);
		}
		characterAddBuff(FOCUS_ATTACK_MONSTER_BUFF, monster, null);
	}
}