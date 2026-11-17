using UnityEngine;

// 热更层通用委托集合:供 Buff/触发器/伤害/子弹/拖拽等系统回调使用(此文件与 Game 层同名文件互相独立)。
public delegate void AddBuffCallback(bool result, CharacterTrigger buffTrigger, long buffTriggerAssignID);
// 触发器条件判定/回调:检查角色是否满足触发条件,满足后执行加 Buff 等逻辑。
public delegate bool BuffTriggerCheck(Character character, CharacterTrigger buffTrigger);
public delegate void BuffTriggerCallback(Character character, CharacterTrigger buffTrigger);
public delegate void BuffTriggerAddBuffCallback(Character character, CharacterTrigger buffTrigger, CharacterBuffParam param);
public delegate void OnObjectItemDragDrop(ExcelData item, Vector3 pos);
// 伤害计算回调:签名与 GameUtilityHotFix.generateDamage 一致,返回伤害值并输出命中/暴击/血量变化类型。
public delegate int DamageCallback(CharacterGame target, CharacterGame attacker, SkillBullet bullet, out bool isHit, out bool isCritical, out HP_DELTA deltaType);
public delegate void HitCallback(CharacterGame target, CharacterGame attacker, SkillBullet bullet);
public delegate void BulletCallback(SkillBullet bullet);
public delegate Vector3 StartPositionCallback(SkillBullet bullet);
public delegate int TriggerProbabilityCallback(CharacterTrigger trigger);
public delegate string ItemDescRegisteCallback(int value);