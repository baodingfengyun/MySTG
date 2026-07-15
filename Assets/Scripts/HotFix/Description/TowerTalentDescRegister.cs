using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;

public class TowerTalentDescRegister : DescRegisterBase
{
	protected static TowerTalentDescRegister mInstance;
	public static void registeAll()
	{
		mInstance ??= new TowerTalentDescRegister();
		mInstance.registeAllInternal();
	}
	protected override void registeAllInternal()
	{
		registeDescriptionCallback(1, register_1);		// 十字弓伤害增加
		registeDescriptionCallback(2, register_2);		// 十字弓攻击间隔减少
		registeDescriptionCallback(3, register_3);		// 火炮塔伤害增加
		registeDescriptionCallback(4, register_4);		// 火炮塔攻击爆炸范围增加
		registeDescriptionCallback(5, register_5);		// 天空之矛伤害增加
		registeDescriptionCallback(6, register_6);		// 天空之矛攻击间隔减少
		registeDescriptionCallback(7, register_7);		// 防空飞弹塔伤害增加
		registeDescriptionCallback(8, register_8);		// 防空飞弹塔攻击间隔减少
		registeDescriptionCallback(9, register_9);		// 投石机伤害增加
		registeDescriptionCallback(10, register_10);	// 投石机爆炸范围增加
		registeDescriptionCallback(11, register_11);	// 镶嵌塔伤害增加
		registeDescriptionCallback(12, register_12);	// 镶嵌塔攻击间隔减少
		registeDescriptionCallback(13, register_13);	// 球形飞弹塔伤害增加
		registeDescriptionCallback(14, register_14);	// 球形飞弹塔攻击间隔减少
		registeDescriptionCallback(15, register_15);	// 霰弹塔伤害增加
		registeDescriptionCallback(16, register_16);	// 霰弹塔攻击间隔减少
		registeDescriptionCallback(17, register_17);	// 飞镖发射器伤害增加
		registeDescriptionCallback(18, register_18);	// 飞镖发射器攻击间隔减少
		registeDescriptionCallback(19, register_19);	// 气球炸弹塔伤害增加
		registeDescriptionCallback(20, register_20);	// 气球炸弹塔爆炸范围增加
		registeDescriptionCallback(21, register_21);	// 震荡塔伤害增加
		registeDescriptionCallback(22, register_22);	// 震荡塔能量球生成上限增加
		registeDescriptionCallback(23, register_23);	// 波动塔伤害增加
		registeDescriptionCallback(24, register_24);	// 波动塔攻击间隔减少
		registeDescriptionCallback(25, register_25);	// 十字弓释放技能时有几率连续攻击2次
		registeDescriptionCallback(26, register_26);	// 十字弓每攻击6次就会对范围内目标造成一次伤害
		registeDescriptionCallback(27, register_27);	// 十字弓每攻击3次后下一击必定暴击
		registeDescriptionCallback(28, register_28);	// 十字弓使英雄攻击提升,攻速提升
		registeDescriptionCallback(29, register_29);	// 十字弓前5波伤害提升
		registeDescriptionCallback(30, register_30);	// 防空飞弹塔命中时有几率击退
		registeDescriptionCallback(31, register_31);	// 防空飞弹塔子弹数量增加, 攻速降低
		registeDescriptionCallback(32, register_32);	// 防空飞弹塔攻速怎加,射程降低
		registeDescriptionCallback(33, register_33);	// 防空飞弹塔击杀敌人后获得攻击提升,攻速提升
		registeDescriptionCallback(34, register_34);	// 防空飞弹塔每一定时间获得一次攻速增加的buff
		registeDescriptionCallback(35, register_35);	// 投石机射程增加
		registeDescriptionCallback(36, register_36);	// 投石机子弹飞行距离越远,爆炸范围越大
		registeDescriptionCallback(37, register_37);	// 投石机子弹飞行距离越远, 伤害越大
		registeDescriptionCallback(38, register_38);	// 投石机子弹对爆炸中心一定范围内的敌人伤害增加
		registeDescriptionCallback(39, register_39);	// 投石机子弹飞行速度增加, 伤害增加
		registeDescriptionCallback(40, register_40);	// 投石机命中时有几率眩晕敌人
		registeDescriptionCallback(41, register_41);	// 镶嵌塔紫宝石技能弹射目标数量增加
		registeDescriptionCallback(42, register_42);	// 火炮塔发射的火炮数量增加，AOE范围减少
		registeDescriptionCallback(43, register_43);	// 火炮塔发射的火炮数量增加，伤害降低
		registeDescriptionCallback(44, register_44);	// 塔附近1格有n个塔，每个塔增加该塔的攻击
		registeDescriptionCallback(45, register_45);	// 塔附近1格有n个塔，每个塔增加该塔的攻速
		registeDescriptionCallback(46, register_46);	// 球形飞弹塔攻击发射的子弹数增加，但是攻击间隔提升
		registeDescriptionCallback(47, register_47);	// 球形飞弹塔子弹命中敌方单位的时候，会添加一个有时效的减速
		registeDescriptionCallback(48, register_48);	// 霰弹塔每次攻击会击退敌方单位
		registeDescriptionCallback(49, register_49);	// 天空之矛击中目标血量超过百分比，晕眩一段时间，BOSS时间减半
		registeDescriptionCallback(50, register_50);	// 被天空之矛攻击的单位防御下降一段时间
		registeDescriptionCallback(51, register_51);	// 天空之矛攻击有概率触发斩杀，击杀血量小于血量百分比的单位
		registeDescriptionCallback(52, register_52);	// 天空之矛攻击精英/首领怪物时，攻击增加100%
		registeDescriptionCallback(53, register_53);	// 飞镖发射器初始攻击间隔提高，每次攻击降低攻击间隔，有叠加上限，若一定时间内未进行攻击则重置
		registeDescriptionCallback(54, register_54);	// 每波开始，飞镖发射器有一定概率进入过载状态，暴击概率提升，暴击伤害提高
		registeDescriptionCallback(55, register_55);	// 气球炸弹塔爆炸范围有概率造成晕眩
		registeDescriptionCallback(56, register_56);	// 气球炸弹塔爆炸范围提升，但是攻击力下降
		registeDescriptionCallback(57, register_57);	// 气球炸弹塔一段时间内没有造成伤害，下次攻击子弹增加
		registeDescriptionCallback(58, register_58);	// 气球炸弹塔击杀的单位会对附近的空中单位造成此单位最大生命百分比的伤害
		registeDescriptionCallback(59, register_59);	// 震荡塔能量球的旋转速度提升百分比
		registeDescriptionCallback(60, register_60);	// 波动塔的索敌范围提升
		registeDescriptionCallback(61, register_61);	// 波动塔子弹增加，但是攻击间隔增加
		registeDescriptionCallback(62, register_62);	// 球形飞弹塔子弹数翻倍，但是攻击下降
		registeDescriptionCallback(63, register_63);	// 球形飞弹塔释放一定数量子弹，攻速提升，直到波次结束
		registeDescriptionCallback(64, register_64);	// 霰弹塔波次开始的一定时间内，攻速提升
		registeDescriptionCallback(65, register_65);	// 霰弹塔射程增加
		registeDescriptionCallback(66, register_66);	// 霰弹塔每击杀n个敌人，获得建造点
		registeDescriptionCallback(67, register_67);	// 霰弹塔击杀n个敌人后，下一次攻击提高
		registeDescriptionCallback(68, register_68);	// 天空之矛每击杀n个敌人，回复m点已损失的羊村生命
		registeDescriptionCallback(69, register_69);	// 飞镖发射器攻击力降低，每次攻击提高攻击力。上限n层，切换目标时重置
		registeDescriptionCallback(70, register_70);	// 飞镖发射器攻击力提高，但每次攻击都会随机选择射程内的目标
		registeDescriptionCallback(71, register_71);	// 飞镖发射器对处于异常状态下的单位造成的伤害提升
		registeDescriptionCallback(72, register_72);	// 气球炸弹塔击杀n个敌人后，升级将变为免费
		registeDescriptionCallback(73, register_73);	// 波动塔子弹大小提升，伤害提升
		registeDescriptionCallback(74, register_74);	// 波动塔子弹每穿过一个敌方单位，子弹攻击提高
		registeDescriptionCallback(75, register_75);	// 2级波动塔获得1点飞行距离，3级波动塔获得3点飞行距离
		registeDescriptionCallback(76, register_76);	// 防空飞弹塔的子弹会弹跳攻击n次，每次弹跳子弹的伤害降低
		registeDescriptionCallback(77, register_77);	// 十字弓击杀n个敌人后，随机变成一个同等级的其他防御塔
		registeDescriptionCallback(78, register_78);	// 火炮塔炮弹着陆时有概率爆炸多次
		registeDescriptionCallback(79, register_79);	// 震荡塔子弹碰撞后不消失，但是伤害降低
		registeDescriptionCallback(80, register_80);	// 震荡塔射程内的怪物移动速度降低，多个震荡塔不叠加
		registeDescriptionCallback(81, register_81);	// 震荡塔击杀n个敌人，获得能量球
		registeDescriptionCallback(82, register_82);	// 震荡塔能量球生成间隔降低
		registeDescriptionCallback(83, register_83);	// 震荡塔每有n个能量球消失，一定范围的塔暴击提升，持续一段时间，有最高叠层
		registeDescriptionCallback(84, register_84);	// 火炮塔子弹爆炸后会留下一个减速地面，持续n秒
		registeDescriptionCallback(85, register_85);	// 回旋镖塔子弹体积增大
		registeDescriptionCallback(86, register_86);	// 回旋镖塔范围增大，飞行速度增加
		registeDescriptionCallback(87, register_87);	// 回旋镖塔子弹增加，攻速降低
		registeDescriptionCallback(88, register_88);	// 回旋镖塔每击杀n个单位，攻速增加
		registeDescriptionCallback(89, register_89);	// 飞镖发射器子弹弹跳次数增加，伤害降低
		registeDescriptionCallback(201, register_201);	// 十字弓伤害增加
		registeDescriptionCallback(211, register_211);	// 天空之矛伤害增加
		registeDescriptionCallback(221, register_221);	// 防空飞弹塔每一定时间获得一次攻速增加的buff
		registeDescriptionCallback(231, register_231);	// 波动塔的索敌范围提升
		registeDescriptionCallback(241, register_241);	// 震荡塔射程内的怪物移动速度降低，多个震荡塔不叠加
		registeDescriptionCallback(251, register_251);	// 火炮塔子弹爆炸后会留下一个减速地面，持续n秒
		registeDescriptionCallback(261, register_261);	// 霰弹塔击杀n个敌人后，下一次攻击提高
		registeDescriptionCallback(271, register_271);	// 防空飞弹塔的子弹会弹跳攻击n次，每次弹跳子弹的伤害降低
		registeDescriptionCallback(281, register_281);	// 球形飞弹塔子弹命中敌方单位的时候，会添加一个有时效的减速
		registeDescriptionCallback(291, register_291);	// 气球炸弹塔击杀n个敌人后，升级将变为免费
		registeDescriptionCallback(9001, register_9001);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9002, register_9002);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9003, register_9003);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9004, register_9004);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9005, register_9005);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9006, register_9006);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9007, register_9007);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9008, register_9008);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9009, register_9009);	// 肉鸽模式中解锁塔
		registeDescriptionCallback(9010, register_9010);	// 肉鸽模式中解锁塔
	}
	public static void checkAll()
	{
		foreach (EDTowerTalent item in mExcelTowerTalent.queryAll())
		{
			if (!mInstance.mRegisteCallbackList.TryGetValue(item.mID, out ItemDescRegisteCallback callback))
			{
				logError("天赋描述缺失:" + item.mID);
				continue;
			}
			log(callback(item.mID));
		}
		foreach (var item in mInstance.mRegisteCallbackList)
		{
			log(item.Value(item.Key));
		}
	}
	// 获取到的描述,是已经经过了多语言转换以后的字符串
	public static string getDescLocalized(int itemID)
	{
		return mInstance.mRegisteCallbackList.get(itemID)?.Invoke(itemID);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static string register_1(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_2(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_3(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_4(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffTowerExploRangeUpParam>(out var exploRangeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), exploRangeUp.mIncrease.toPercent());
	}
	protected static string register_5(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_6(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_7(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_8(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_9(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_10(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffTowerExploRangeUpParam>(out var exploRangeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), exploRangeUp.mIncrease.toPercent());
	}
	protected static string register_11(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_12(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_13(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_14(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_15(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_16(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_17(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_18(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_19(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_20(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffTowerExploRangeUpParam>(out var exploRangeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), exploRangeUp.mIncrease.toPercent());
	}
	protected static string register_21(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_22(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffZhenDangTaBulletCountUpParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS());
	}
	protected static string register_23(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_24(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_25(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerFireSkillParam>(out var triggerResetSkillCD, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerResetSkillCD.mProbability.toProbability());
	}
	protected static string register_26(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerFireCountSkillHitParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), (triggerHit.mFireCount - 1).IToS(), triggerHit.mFireCount.IToS());
	}
	protected static string register_27(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerFireCountSkillWillGenerateDamageParam>(out var triggerWillHit, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerWillHit.mFireCount.IToS());
	}
	protected static string register_28(int id)
	{
		return "";
	}
	protected static string register_29(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpBeforeWaveParam>(out var damageUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), damageUp.mWaveCount.IToS(), damageUp.mIncrease.toPercent());
	}
	protected static string register_30(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffStrickBackParam>(out var strickBack, triggerHit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerHit.mProbability.toProbability(), strickBack.mGridCount.FToS());
	}
	protected static string register_31(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedDownParam>(out var attackSpeedDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS(), attackSpeedDown.mDecreaseAttackSpeed.toPercent());
	}
	protected static string register_32(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffSkillRangeDownParam>(out var skillRangeDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent(), skillRangeDown.mDecreasePercent.toPercent());
	}
	protected static string register_33(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerKillMonsterParam>(out var triggerKill, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackUpParam>(out var attackUp, triggerKill.mBuffDetailIDList[0]);
		using var d = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, triggerKill.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackUp.mPercent.toPercent(), attackSpeedUp.mIncreaseAttackSpeed.toPercent(), attackUp.mBuffTime.FToS());
	}
	protected static string register_34(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerTimeIntervalParam>(out var triggerTimeInterval, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, triggerTimeInterval.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerTimeInterval.mCD.FToS(), attackSpeedUp.mIncreaseAttackSpeed.toPercent(), attackSpeedUp.mBuffTime.FToS());
	}
	protected static string register_35(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffSkillRangeUpParam>(out var skillRangeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), skillRangeUp.mIncreasePercent.toPercent());
	}
	protected static string register_36(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletFlyDisIncreaseExploRangeParam>(out var distanceIncreaseExploRange, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), distanceIncreaseExploRange.mIncreaseRangePercent.toPercent());
	}
	protected static string register_37(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletFlyDisIncreaseDamageParam>(out var distanceIncreaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), distanceIncreaseDamage.mIncreasePercent.toPercent());
	}
	protected static string register_38(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletDamageUpInExploRangeParam>(out var damageUpInRange, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), damageUpInRange.mRangePercent.toPercent(), damageUpInRange.mIncreaseDamage.toPercent());
	}
	protected static string register_39(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletSpeedUpParam>(out var bulletSpeedUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffDamageUpParam>(out var damageUp, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletSpeedUp.mIncreasePercent.toPercent(), damageUp.mIncrease.toPercent());
	}
	protected static string register_40(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffToBulletExploRangeParam>(out var triggerToExploRange, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffVertigoParam>(out var vertigo, triggerToExploRange.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerToExploRange.mProbability.toProbability(), vertigo.mBuffTime.FToS());
	}
	protected static string register_41(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffXiangQianTaPurpleGemTargetCountUpParam>(out var targetCountUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), targetCountUp.mIncreaseCount.IToS());
	}
	protected static string register_42(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountHuoPaoParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffTowerExploRangeDownParam>(out var exploRangeDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS(), exploRangeDown.mIncrease.toPercent());
	}
	protected static string register_43(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountHuoPaoParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffDamageDownParam>(out var damageDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS(), damageDown.mDecrease.toPercent());
	}
	protected static string register_44(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAroundTowerCountAttackUpParam>(out var atkUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), atkUp.mPercent.toPercent());
	}
	protected static string register_45(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAroundTowerCountAttackSpeedUpParam>(out var atkSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), atkSpeedUp.mPercent.toPercent());
	}
	protected static string register_46(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedDownParam>(out var attackSpeedDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS(), attackSpeedDown.mDecreaseAttackSpeed.toPercent());
	}
	protected static string register_47(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var hitTrigger, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffMoveSpeedDownRougeParam>(out var moveSpeedDown, hitTrigger.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), moveSpeedDown.mPercent.toPercent(), moveSpeedDown.mBuffTime.FToS());
	}
	protected static string register_48(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffStrickBackParam>(out var strickBack, triggerHit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), strickBack.mGridCount.FToS());
	}
	protected static string register_49(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerWillHitHpMinPercentParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffVertigoParam>(out var vertigo, triggerHit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerHit.mPercent.toPercent(), vertigo.mBuffTime.FToS());
	}
	protected static string register_50(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffDefenceDownPercentParam>(out var defenceDown, triggerHit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), defenceDown.mPercent.toPercent(), defenceDown.mBuffTime.FToS());
	}
	protected static string register_51(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerWillHitHpMaxPercentParam>(out var triggerHit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffInstantDeathParam>(out var death, triggerHit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerHit.mPercent.toPercent(), triggerHit.mProbability.toProbability());
	}
	protected static string register_52(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpStrengthMonsterParam>(out var damageUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), damageUp.mPercent.toPercent());
	}
	protected static string register_53(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpSteppedParam>(out var speedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), speedUp.mDecrease.toPercent(), speedUp.mIncrease.toPercent(), speedUp.mLayerMax.IToS(), speedUp.mTimeMax.FToS());
	}
	protected static string register_54(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffOnWaveStartParam>(out var waveStart, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffCriticalUpParam>(out var critical, waveStart.mBuffDetailIDList[0]);
		using var d = new BuffParamScopeT<BuffCriticalDamageUpParam>(out var criticalDamage, waveStart.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), waveStart.mProbability.toProbability(), critical.mIncrease.toPercent(), criticalDamage.mIncrease.toPercent());
	}
	protected static string register_55(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var hit, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffVertigoParam>(out var vertigo, hit.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), hit.mProbability.toProbability(), vertigo.mBuffTime.FToS());
	}
	protected static string register_56(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffTowerExploRangeUpParam>(out var exploRange, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackDownParam>(out var attackDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), exploRange.mIncrease.toPercent(), attackDown.mPercent.toPercent());
	}
	protected static string register_57(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountByNoDamageTimeParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mTimeMax.FToS(), bulletCountUp.mIncreaseCount.IToS());
	}
	protected static string register_58(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffRangeDamageByHpMaxPercentParam>(out var rangeDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), rangeDamage.mRange.FToS(), rangeDamage.mPercent.toPercent());
	}
	protected static string register_59(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletSpeedUpParam>(out var bulletSpeed, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletSpeed.mIncreasePercent.toPercent());
	}
	protected static string register_60(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffSkillRangeUpParam>(out var range, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), range.mIncreasePercent.toPercent());
	}
	protected static string register_61(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountParam>(out var bulletAdd, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedDownParam>(out var attackSpeed, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletAdd.mIncreaseCount.IToS(), attackSpeed.mDecreaseAttackSpeed.toPercent());
	}
	protected static string register_62(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackDownParam>(out var attackDown, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffIncreaseBulletCountPercentParam>(out var bulletCountPercent, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountPercent.mIncreasePercent.IToS(), attackDown.mPercent.toPercent());
	}
	protected static string register_63(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffByWaveBulletCountParam>(out var bulletCount, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, bulletCount.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCount.mBulletCount.IToS(), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_64(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffOnWaveStartParam>(out var waveStart, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, waveStart.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mBuffTime.FToS(), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_65(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffSkillRangeUpParam>(out var skillRangeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), skillRangeUp.mIncreasePercent.toPercent());
	}
	protected static string register_66(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffRogueKillMonsterAddBuildCoinParam>(out var killMonster, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), killMonster.mNeedCount.IToS(), killMonster.mAddCoin.IToS());
	}
	protected static string register_67(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackUpOnceByKillMonsterParam>(out var killMonster, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), killMonster.mNeedCount.IToS(), killMonster.mIncreasePercent.toPercent());
	}
	protected static string register_68(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffRogueKillMonsterCureLevelHpParam>(out var killMonster, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), killMonster.mNeedCount.IToS(), killMonster.mCureHp.IToS());
	}
	protected static string register_69(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackDownThenUpByHitSameMonsterParam>(out var hitMonster, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), hitMonster.mDownPercent.toPercent(), hitMonster.mUpPercent.toPercent(), hitMonster.mMaxLayer.IToS());
	}
	protected static string register_70(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackUpParam>(out var attackUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackUp.mPercent.toPercent());
	}
	protected static string register_71(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpToDebuffMonsterParam>(out var damageUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), damageUp.mIncrease.toPercent());
	}
	protected static string register_72(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffRogueKillMonsterFreeUpLevelParam>(out var freeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), freeUp.mNeedCount.IToS());
	}
	protected static string register_73(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffScaleBulletParam>(out var scale, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffDamageUpParam>(out var damageUp, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), scale.mScale.toPercent(), damageUp.mIncrease.toPercent());
	}
	protected static string register_74(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffBulletAttackUpHitMonsterParam>(out var attackUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackUp.mPercent.toPercent());
	}
	protected static string register_75(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseFlyDisByRogueTowerLevelParam>(out var flyDis, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType),
            flyDis.mLevels[0].IToS(), flyDis.mAddDis[0].FToS(), flyDis.mLevels[1].IToS(), flyDis.mAddDis[1].FToS());
	}
	protected static string register_76(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletBounceTimesParam>(out var bounceTimes, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bounceTimes.mIncreaseCount.IToS(), bounceTimes.mIncreaseDamagePercent.toPercent());
	}
	protected static string register_77(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffKillMonsterChangeRandomTowerParam>(out var changeTower, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), changeTower.mNeedCount.IToS());
	}
	protected static string register_78(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseHuoPaoExplosionMultiParam>(out var bounceTimes, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bounceTimes.mIncreaseChance.toPercent(), bounceTimes.mIncreaseCount.IToS());
	}
	protected static string register_79(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageDownParam>(out var damageDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), damageDown.mDecrease.toPercent());
	}
	protected static string register_80(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffByEnterTowerRangeParam>(out var trigger, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffMoveSpeedDownZhenDownTowerRangeParam>(out var speedDown, trigger.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), speedDown.mPercent.toPercent());
	}
	protected static string register_81(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffZhenDangAddBulletByKillMonsterParam>(out var addBullet, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), addBullet.mNeedCount.IToS(), addBullet.mAddCount.IToS());
	}
	protected static string register_82(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_83(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffWhenBulletConsumeParam>(out var bulletConsume, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<TriggerBuffToHexRangeTowerParam>(out var hexRange, bulletConsume.mBuffDetailIDList[0]);
		using var d = new BuffParamScopeT<BuffZhenDangConsumeCriticalUpParam>(out var zhenDangCritical, hexRange.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType)
			, bulletConsume.mMaxCount.IToS(), hexRange.mRange.IToS(), zhenDangCritical.mIncrease.toPercent(), zhenDangCritical.mBuffTime.FToS(), zhenDangCritical.mLayerMax.IToS());
	}
	protected static string register_84(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffGlobalWhenBulletExplosionHuoPaoParam>(out var global, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<TriggerBuffWithAreaColliderParam>(out var area, global.mBuffDetailIDList[0]);
		using var d = new BuffParamScopeT<BuffMoveSpeedDownHuoPaoExplosionAreaParam>(out var speedDown, area.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), speedDown.mPercent.toPercent(), area.mBuffTime.FToS());
	}
	protected static string register_85(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffScaleBulletParam>(out var scaleButtlet, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), scaleButtlet.mScale.toPercent());
	}
	protected static string register_86(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffSkillRangeUpParam>(out var range, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffBulletSpeedUpParam>(out var flySpeed, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), range.mIncreasePercent.toPercent(), flySpeed.mIncreasePercent.toPercent());
	}
	protected static string register_87(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountParam>(out var bulletCount, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedDownParam>(out var speedDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCount.mIncreaseCount.IToS(), speedDown.mDecreaseAttackSpeed.toPercent());
	}
	protected static string register_88(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpByKillMonsterParam>(out var attackSpeedUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), attackSpeedUp.mNeedCount.IToS(), attackSpeedUp.mAddSpeed.toPercent(), attackSpeedUp.mMaxSpeed.toPercent());
	}
	protected static string register_89(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletBounceTimesParam>(out var bounceCount, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffDamageDownParam>(out var damageDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bounceCount.mIncreaseCount.IToS(), damageDown.mDecrease.toPercent());
	}
	protected static string register_201(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_211(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffDamageUpParam>(out var increaseDamage, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), increaseDamage.mIncrease.toPercent());
	}
	protected static string register_221(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerTimeIntervalParam>(out var triggerTimeInterval, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var attackSpeedUp, triggerTimeInterval.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), triggerTimeInterval.mCD.FToS(), attackSpeedUp.mIncreaseAttackSpeed.toPercent(), attackSpeedUp.mBuffTime.FToS());
	}
	protected static string register_231(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffSkillRangeUpParam>(out var range, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), range.mIncreasePercent.toPercent());
	}
	protected static string register_241(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerBuffByEnterTowerRangeParam>(out var trigger, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffMoveSpeedDownZhenDownTowerRangeParam>(out var speedDown, trigger.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), speedDown.mPercent.toPercent());
	}
	protected static string register_251(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffIncreaseBulletCountHuoPaoParam>(out var bulletCountUp, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffTowerExploRangeDownParam>(out var exploRangeDown, buff.mBuffDetailIDList[1]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), bulletCountUp.mIncreaseCount.IToS(), exploRangeDown.mIncrease.toPercent());
	}
	protected static string register_261(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackUpOnceByKillMonsterParam>(out var killMonster, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), killMonster.mNeedCount.IToS(), killMonster.mIncreasePercent.toPercent());
	}
	protected static string register_271(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffAttackSpeedUpParam>(out var speed, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), speed.mIncreaseAttackSpeed.toPercent());
	}
	protected static string register_281(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<TriggerHitParam>(out var hitTrigger, buff.mBuffDetailIDList[0]);
		using var c = new BuffParamScopeT<BuffMoveSpeedDownRougeParam>(out var moveSpeedDown, hitTrigger.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), moveSpeedDown.mPercent.toPercent(), moveSpeedDown.mBuffTime.FToS());
	}
	protected static string register_291(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<TriggerBuffToTypeTowerParam>(out var buff, data.mBuff[0]);
		using var b = new BuffParamScopeT<BuffRogueKillMonsterFreeUpLevelParam>(out var freeUp, buff.mBuffDetailIDList[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTowerType), freeUp.mNeedCount.IToS());
	}
	protected static string register_9001(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9002(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9003(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9004(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9005(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9006(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9007(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9008(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9009(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
	protected static string register_9010(int id)
	{
		EDTowerTalent data = talentData(id);
		using var a = new BuffParamScopeT<BuffAddTowerRogueParam>(out var buff, data.mBuff[0]);
		return mLocalizationManager.getLocalize(data.mDescription, mExcelTower.getTowerName(buff.mTower));
	}
}