using UnityEngine;
using static FrameBaseUtility;

// 气球炸弹塔（继承自防御塔）
public class CharacterTowerQiQiuZhaDan : CharacterTower
{
	protected Animator mBalloonAnimator;			// 气球动画
	public override void resetProperty()
	{
		base.resetProperty();
		mBalloonAnimator = null;
	}
	public override void initData(EDTower towerData)
	{
		base.initData(towerData);
		mComAvatar.setModelInitedCallback((Character character) =>
		{
			GameObject balloonObject = findGameObject("P_QiQiu", mComAvatar.getModel(), true);
			mBalloonAnimator = balloonObject.GetComponentInChildren<Animator>();
		});
	}
	public Animator getBalloonAnimator() { return mBalloonAnimator; }
}