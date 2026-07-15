
// 参数
public class BuffClearDebuffParam : CharacterBuffParamT<BuffClearDebuffParam>
{
	public override void registeAllParam() {}
	public override void check() { }
}

// 清除所有debuff
public class BuffClearDebuff : CharacterBuffT<BuffClearDebuffParam>
{}