using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIHPBar.prefab
// 血条界面
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