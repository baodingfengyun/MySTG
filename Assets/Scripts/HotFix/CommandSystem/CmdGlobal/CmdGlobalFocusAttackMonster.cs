using static GBR;
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
		characterAddBuff(EDBuffDetail.FOCUS_ATTACK_MONSTER_ID, monster, null);
	}
}