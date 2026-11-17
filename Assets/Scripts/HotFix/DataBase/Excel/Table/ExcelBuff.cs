
// Buff 主表：登记每种 Buff 的类型枚举（对应 StateRegister 里反射注册的状态类）。
// 真正的参数在 ExcelBuffDetail；本表只做类型登记，无查询缓存与自定义校验。
public class ExcelBuff : ExcelTableT<EDBuff>
{
    // auto generate start
	protected override void checkAllDataDefault() {}
    // auto generate end
}