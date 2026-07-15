// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Guide表格
public class EDGuide : ExcelDataT<EDGuide>
{
	public int mType;								// 类型
	public int mGroupID;							// 引导组ID
	public int mFallbackID;							// 引导中断时的回溯步骤ID
	public bool mDeactiveInputAtFinish;				// 完成此步骤后是否禁用输入检测
	public string mTip;								// 提示文字
	public int mTipLocID;							// 提示文字多语言ID
	public int mTipPosition;						// 提示文字的位置
	public int mNPCPosition;						// NPC的位置
	public int mTalkBackground;						// 对话框背景样式
	public int mTalkPosition;						// 对话框位置
	public bool mNeedClickTalk;						// 是否需要点击对话
	public string mNPCTalk;							// NPC说的话
	public int mNPCTalkLocID;						// NPC说的话多语言ID
	public int mClickStyle;							// 点击提示手指的样式
	public string mParam0;							// 参数0
	public string mParam1;							// 参数1
	public string mParam2;							// 参数2
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.read(out mType);
		result = result && reader.read(out mGroupID);
		result = result && reader.read(out mFallbackID);
		result = result && reader.read(out mDeactiveInputAtFinish);
		result = result && reader.readString(out mTip);
		result = result && reader.read(out mTipLocID);
		result = result && reader.read(out mTipPosition);
		result = result && reader.read(out mNPCPosition);
		result = result && reader.read(out mTalkBackground);
		result = result && reader.read(out mTalkPosition);
		result = result && reader.read(out mNeedClickTalk);
		result = result && reader.readString(out mNPCTalk);
		result = result && reader.read(out mNPCTalkLocID);
		result = result && reader.read(out mClickStyle);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParam1);
		result = result && reader.readString(out mParam2);
		return result;
	}
}
// auto generate end