
// 客户端数据
public class ClientSystem : FrameSystem
{
	protected COMClientUserData mCOMUserData;                       // 玩家账号信息
	protected COMClientLevel mCOMLevel;                             // 关卡通关数据
	protected COMClientTime mCOMClientTime;							// 时间
	public void clear()
	{
		foreach (GameComponent com in getAllComponent().getMainList().Values)
		{
			if (com is IClientSystemComponent comInterface)
			{
				comInterface.clear();
			}
		}
	}
	public COMClientUserData getCOMUserData()						{ return mCOMUserData; }
	public COMClientLevel getCOMLevel()								{ return mCOMLevel; }
	public COMClientTime getCOMClientTime()							{ return mCOMClientTime; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initComponents()
	{
		base.initComponents();
		addComponent(out mCOMUserData, false);
		addComponent(out mCOMLevel, false);
		addComponent(out mCOMClientTime, true);
	}
}