
// 参数
public class BuffHoldPositionParam : CharacterBuffParamT<BuffHoldPositionParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 禁锢,不允许移动
public class BuffHoldPosition : CharacterBuffT<BuffHoldPositionParam>
{}