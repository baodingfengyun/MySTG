
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