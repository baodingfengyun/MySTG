using Obfuz;
using System;
using static GBR;

// 热更层顶层管理器
[ObfuzIgnore]
public class GameHotFix : GameHotFixBase<GameHotFix>
{
    protected override void registerAllTable()
    {
        ExcelRegister.registeAll();
    }
    protected override void registerAll()
    {
        LayoutRegisterHotFix.registerAll();
        SceneRegister.registerAll();
        StateRegister.registerAll();
        StateGroupRegister.registerAll();
        BulletRegister.registerAll();
        TowerRegister.registerAll();
        TowerSkillRegister.registeSkill();
        TowerTalentDescRegister.registeAll();
        MonsterSkillRegister.registeAll();
        ItemDescRegister.registeAll();
        BulletDamageModifierRegister.registerAll();
    }
    protected override void initFrameSystem()
    {
        registeFrameSystem<TowerDefenceSystem>(com =>       mTowerDefenceSystem = com);
        registeFrameSystem<BulletManager>(com =>            mBulletManager = com);
        registeFrameSystem<ClientSystem>(com =>             mClientSystem = com);
        registeFrameSystem<StateManagerHotFix>(com =>       mStateManagerHotFix = com);
        registeFrameSystem<GameLocalizationSystem>(com =>   mGameLocalizationSystem = com);
        registeFrameSystem<GuideSystem>(com =>              mGuideSystem = com);
        registeFrameSystem<RedPointManager>(com =>          mRedPointManager = com);
    }
    protected override Type getStartGameSceneType() { return typeof(GameSceneLogin); }
}