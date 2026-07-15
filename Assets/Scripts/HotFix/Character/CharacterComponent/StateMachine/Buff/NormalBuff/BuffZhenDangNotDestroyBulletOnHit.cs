using static FrameBaseHotFix;

// 参数
public class BuffZhenDangNotDestroyBulletOnHitParam : CharacterBuffParamT<BuffZhenDangNotDestroyBulletOnHitParam>
{
	public override void registeAllParam() {}
	public override void check() {}
}

// 设置震荡塔子弹不消失
public class BuffZhenDangNotDestroyBulletOnHit : CharacterBuffT<BuffZhenDangNotDestroyBulletOnHitParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChanged, this);
		doIncrease();
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame is CharacterTower tower && tower.getComSkill().getCurSkill() is TowerSkill_ZhenDang skill)
		{
			skill.removeNotDestroyBulletOnHit();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChanged(EventTowerSkillChanged param)
	{
		doIncrease();
	}
	protected void doIncrease()
	{
		if ((mCharacterGame as CharacterTower).getComSkill().getCurSkill() is TowerSkill_ZhenDang skill)
		{
			skill.addNotDestroyBulletOnHit();
		}
	}
}