// auto generate start
using System;
using static GBR;
using static FrameBaseHotFix;

public class ExcelRegister
{
	public static void registeAll()
	{
		registeTable(out mExcelAudio, typeof(EDAudio), "Audio");
		registeTable(out mExcelBuff, typeof(EDBuff), "Buff");
		registeTable(out mExcelBuffDetail, typeof(EDBuffDetail), "BuffDetail");
		registeTable(out mExcelBulletDamageModifier, typeof(EDBulletDamageModifier), "BulletDamageModifier");
		registeTable(out mExcelCardPoolConfig, typeof(EDCardPoolConfig), "CardPoolConfig");
		registeTable(out mExcelChapter, typeof(EDChapter), "Chapter");
		registeTable(out mExcelEffect, typeof(EDEffect), "Effect");
		registeTable(out mExcelGlobalConfig, typeof(EDGlobalConfig), "GlobalConfig");
		registeTable(out mExcelGridPrefab, typeof(EDGridPrefab), "GridPrefab");
		registeTable(out mExcelGuide, typeof(EDGuide), "Guide");
		registeTable(out mExcelLevel, typeof(EDLevel), "Level");
		registeTable(out mExcelLocalization, typeof(EDLocalization), "Localization");
		registeTable(out mExcelMapConfig, typeof(EDMapConfig), "MapConfig");
		registeTable(out mExcelMapPortal, typeof(EDMapPortal), "MapPortal");
		registeTable(out mExcelMonster, typeof(EDMonster), "Monster");
		registeTable(out mExcelMonsterSkill, typeof(EDMonsterSkill), "MonsterSkill");
		registeTable(out mExcelRewardConfig, typeof(EDRewardConfig), "RewardConfig");
		registeTable(out mExcelSkillBullet, typeof(EDSkillBullet), "SkillBullet");
		registeTable(out mExcelTower, typeof(EDTower), "Tower");
		registeTable(out mExcelTowerSkill, typeof(EDTowerSkill), "TowerSkill");
		registeTable(out mExcelTowerTalent, typeof(EDTowerTalent), "TowerTalent");
		registeTable(out mExcelWaveConfig, typeof(EDWaveConfig), "WaveConfig");

		// 进入热更以后,所有资源都处于可用状态
		mExcelManager.resourceAvailable();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeTable<T>(out T table, Type dataType, string tableName) where T : ExcelTable
	{
		table = mExcelManager.registe(tableName, typeof(T), dataType) as T;
	}
}
// auto generate end