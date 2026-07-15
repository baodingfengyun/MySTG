
// 参数
public class BuffAntiStrickBackParam : CharacterBuffParamT<BuffAntiStrickBackParam>
{
	public override void registeAllParam() {}
	public override void check() { }
}

// 无法被击退
public class BuffAntiStrickBack : CharacterBuffT<BuffAntiStrickBackParam>
{}