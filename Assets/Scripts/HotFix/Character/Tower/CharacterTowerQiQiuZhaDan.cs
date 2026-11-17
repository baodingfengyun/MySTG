using UnityEngine;
using static FrameBaseUtility;

// 气球炸弹塔（继承自防御塔）：唯一"技能与实体类配套"的塔
// 【为什么需要专属实体类】普通塔的攻击动画由基类 Avatar 处理,而本塔的气球有独立的
//   装填(Reload)动画,需要在模型加载完成后抓取 P_QiQiu 节点上的 Animator 供技能层调用。
// 【初始化链】initData → mComAvatar.setModelInitedCallback(模型就绪回调) →
//   在模型里 findGameObject("P_QiQiu") → GetComponentInChildren<Animator> 存到 mBalloonAnimator。
//   注意用回调而非直接查找,因为模型是异步实例化的,initData 时节点还不存在。
// 【技能层配合】TowerSkill_QiQiuZhaDan 通过 mTower.getBalloonAnimator() 拿到它触发装填表现。
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
		// 模型是异步加载的,必须等"模型已初始化"回调触发后节点树才完整,此刻才能找到气球
		mComAvatar.setModelInitedCallback((Character character) =>
		{
			GameObject balloonObject = findGameObject("P_QiQiu", mComAvatar.getModel(), true);
			mBalloonAnimator = balloonObject.GetComponentInChildren<Animator>();
		});
	}
	public Animator getBalloonAnimator() { return mBalloonAnimator; }
}