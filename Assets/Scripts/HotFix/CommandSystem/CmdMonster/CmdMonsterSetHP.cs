using static GBR;
using static MathUtility;
using static UnityUtility;
using static FrameBaseHotFix;

// 设置怪物血量
public class CmdMonsterSetHP
{
	// hp是改变后的血量
	// delta是血量变化量,可正可负
	// showNumber是否显示伤害数字
	public static void execute(CharacterMonster monster, CharacterGame attacker, int hp, int delta = 0, bool showNumber = false, HP_DELTA deltaType = HP_DELTA.NORMAL_DAMAGE, bool critical = false)
	{
		CharacterMonsterData monsterData = monster.getMonsterData();
		// 如果已经死亡,则不能改变血量
		if (monsterData.mHP <= 0)
		{
			return;
		}
		if (showNumber)
		{
			mUIDamageNumber.showNumber(worldToScreen(monster.getPosition()), abs(delta), deltaType, critical);
		}
		clamp(ref hp, 0, monster.getMaxHP());
		int lastHP = monsterData.mHP;
		monsterData.mHP = hp;
		if (monsterData.mHP <= 0 && attacker != null)
		{
			monsterData.mKillerGUID = attacker.getGUID();
		}
		using var a = new ClassScope<EventMonsterHPChange>(out var param);
		param.mMonster = monster;
		param.mCurHP = hp;
		param.mLastHP = lastHP;
		mEventSystem.pushEvent(param, monster.getGUID());

		updateHPPercent(monster, hp);
	}
	// 初始化血量
	public static void execute(CharacterMonster monster, int hp)
	{
		CharacterMonsterData monsterData = monster.getMonsterData();
		// 如果已经死亡,则不能改变血量
		if (monsterData.mHP <= 0)
		{
			return;
		}
		clampMin(ref hp);
		monsterData.mHP = hp;
		updateHPPercent(monster, hp);
	}
	protected static void updateHPPercent(CharacterMonster monster, int hp)
	{
		float percent = divide(hp, monster.getMaxHP());
		monster.getComAvatar()?.getHPBar()?.setPercent(percent);
		mUIMonsterQueue.safe()?.updateHpBar(monster.getGUID(), percent);
	}
}