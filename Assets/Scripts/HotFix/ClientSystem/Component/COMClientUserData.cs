
// 客户端本地玩家资料组件:缓存用户名、头像、账号、ID 四项,对象池回收时清空。
public class COMClientUserData : GameComponent, IClientSystemComponent
{
	public string mUserName;
	public string mUserHead;
	public string mAccount;
	public string mID;
	public void clear()
	{
		mUserName = null;
		mUserHead = null;
		mAccount = null;
		mID = null;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mUserName = null;
		mUserHead = null;
		mAccount = null;
		mID = null;
	}
}