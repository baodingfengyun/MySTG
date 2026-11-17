using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIHPBar.prefab
// 怪物血条容器：用 MonsterHPBar 对象池管理场景中所有怪物头顶血条——
//   createHPBar 借出一条、destroyHPBar 归还。mNeedUpdate=false 不逐帧 tick，血条位置/比例由外部驱动。
//   UIMonsterQueue 里 Boss 项的血条与此相关。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIHPBar : LayoutScript
{
    protected WindowStructPool<MonsterHPBar> mMonsterHPBarPool;
    // auto generate member end
    public UIHPBar()
	{
		// auto generate constructor start
		mMonsterHPBarPool = new(this);
		// auto generate constructor end
		mNeedUpdate = false;
		mNeedResetAllChild = false;
		mUnuseAllWhenHide = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		mMonsterHPBarPool.assignTemplate(mRoot, "MonsterHPBar");
		// auto generate assignWindow end
	}
	public MonsterHPBar createHPBar()
	{
		return mMonsterHPBarPool.newItem();
	}
	public void destroyHPBar(MonsterHPBar hpbar)
	{
        mMonsterHPBarPool.unuseItem(hpbar);
	}
}