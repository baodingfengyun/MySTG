using UnityEngine;

public delegate void AddBuffCallback(bool result, CharacterTrigger buffTrigger, long buffTriggerAssignID);
public delegate bool BuffTriggerCheck(Character character, CharacterTrigger buffTrigger);
public delegate void BuffTriggerCallback(Character character, CharacterTrigger buffTrigger);
public delegate void BuffTriggerAddBuffCallback(Character character, CharacterTrigger buffTrigger, CharacterBuffParam param);
public delegate void OnObjectItemDragDrop(ExcelData item, Vector3 pos);
public delegate int DamageCallback(CharacterGame target, CharacterGame attacker, SkillBullet bullet, out bool isHit, out bool isCritical, out HP_DELTA deltaType);
public delegate void HitCallback(CharacterGame target, CharacterGame attacker, SkillBullet bullet);
public delegate void BulletCallback(SkillBullet bullet);
public delegate Vector3 StartPositionCallback(SkillBullet bullet);
public delegate int TriggerProbabilityCallback(CharacterTrigger trigger);
public delegate string ItemDescRegisteCallback(int value);