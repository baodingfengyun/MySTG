
// 子弹伤害修改器,只能当作静态对象使用
public class BulletDamageModifier : ClassObject
{
	public virtual void initData(EDBulletDamageModifier data) {}
	public virtual void modify(CharacterGame character, ref int damage){}
}