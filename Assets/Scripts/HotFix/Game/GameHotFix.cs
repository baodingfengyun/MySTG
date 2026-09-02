using Obfuz;
using System;
using static GBR;
using static FrameBaseUtility;

// 热更层顶层管理器
[ObfuzIgnore]
public class GameHotFix : GameHotFixBase<GameHotFix>
{
    protected override void registerAllTable()
    {
        ExcelRegister.registeAll();
        logBase("[游戏热更]注册所有Excel表");
    }
    protected override void registerAll()
    {
        LayoutRegisterHotFix.registerAll();
        logBase("[游戏热更]LayoutRegisterHotFix");
        SceneRegister.registerAll();
        logBase("[游戏热更]SceneRegister");
        StateRegister.registerAll();
        logBase("[游戏热更]StateRegister");
        StateGroupRegister.registerAll();
        logBase("[游戏热更]StateGroupRegister");
        BulletRegister.registerAll();
        logBase("[游戏热更]BulletRegister");
        TowerRegister.registerAll();
        logBase("[游戏热更]TowerRegister");
        TowerSkillRegister.registeSkill();
        logBase("[游戏热更]TowerSkillRegister");
        TowerTalentDescRegister.registeAll();
        logBase("[游戏热更]TowerTalentDescRegister");
        MonsterSkillRegister.registeAll();
        logBase("[游戏热更]MonsterSkillRegister");
        ItemDescRegister.registeAll();
        logBase("[游戏热更]ItemDescRegister");
        BulletDamageModifierRegister.registerAll();
        logBase("[游戏热更]BulletDamageModifierRegister");
    }
    protected override void initFrameSystem()
    {
        registeFrameSystem<TowerDefenceSystem>(com =>       mTowerDefenceSystem = com);
        logBase("[游戏热更]TowerDefenceSystem");
        registeFrameSystem<BulletManager>(com =>            mBulletManager = com);
        logBase("[游戏热更]BulletManager");
        registeFrameSystem<ClientSystem>(com =>             mClientSystem = com);
        logBase("[游戏热更]ClientSystem");
        registeFrameSystem<StateManagerHotFix>(com =>       mStateManagerHotFix = com);
        logBase("[游戏热更]StateManagerHotFix");
        registeFrameSystem<GameLocalizationSystem>(com =>   mGameLocalizationSystem = com);
        logBase("[游戏热更]GameLocalizationSystem");
        registeFrameSystem<GuideSystem>(com =>              mGuideSystem = com);
        logBase("[游戏热更]GuideSystem");
        registeFrameSystem<RedPointManager>(com =>          mRedPointManager = com);
        logBase("[游戏热更]RedPointManager");
    }
    protected override Type getStartGameSceneType() { return typeof(GameSceneLogin); }
}