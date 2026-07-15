using Obfuz;
using UnityEngine;
using static UnityUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIClickEffect.prefab
// 用于显示点击特效
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIClickEffect : LayoutScript
{
	protected myUGUIImageAnim mClickEffect;
    // auto generate member end
    public UIClickEffect()
    {
        // auto generate constructor start
        // auto generate constructor end
    }
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mClickEffect, "ClickEffect");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mRoot.registeCollider(onRootClick, true);
		mClickEffect.setActive(false);
		mClickEffect.setLoop(LOOP_MODE.ONCE);
    }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onRootClick(Vector3 mousePos)
	{
		mClickEffect.setActive(true);
		mClickEffect.setPosition(new Vector2(mousePos.x, mousePos.y) - getHalfScreenSize());
		mClickEffect.stop();
		mClickEffect.play();
	}
}