using System.Collections.Generic;
using static UnityUtility;
using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 热更中用的状态管理器,主要还是用于存储buff的表格数据,避免不必要的重复解析
public class StateManagerHotFix : FrameSystem
{
	protected Dictionary<int, CharacterBuffParam> mStateOriginParamList = new();    // 以EDBuffDetail表格ID为索引,存储各个状态的表格数据转换以后的对象，避免对表格参数的重复解析,不能直接当作添加状态的参数
	public override void init()
	{
		base.init();
		foreach (EDBuffDetail item in mExcelBuffDetail.queryAll())
		{
			mStateOriginParamList.Add(item.mID, createParamByBuffDetail(item));
		}
	}
	// 需要搭配BuffParamScope或者BuffParamScopeT使用,以便于自动销毁参数对象
	public CharacterBuffParam createParam(int buffDetailID)
	{
		if (!mStateOriginParamList.TryGetValue(buffDetailID, out CharacterBuffParam originParam))
		{
			logError("状态参数创建失败:detailID:" + buffDetailID);
			return null;
		}
		EDBuffDetail detailData = originParam.mBuffDetailData;
		ClassObject param = CLASS_ONCE(mStateManager.getParamType(detailData.mBuffTypeID));
		if (param is not CharacterBuffParam buffParam)
		{
			logError("状态参数创建失败:buffID:" + detailData.mBuffTypeID + ", detailID:" + detailData.mID);
			return null;
		}
		buffParam.copy(originParam);
		buffParam.mBuffTime = detailData.mBuffTime;
		buffParam.mBuffData = mExcelBuff.query(detailData.mBuffTypeID);
		buffParam.mBuffDetailData = detailData;
		return buffParam;
	}
	// 此函数获得的参数对象不能用于添加状态,仅用于查看参数值
	public CharacterBuffParam getOriginParam(int buffDetailID) { return mStateOriginParamList.get(buffDetailID); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected CharacterBuffParam createParamByBuffDetail(EDBuffDetail detailData)
	{
		var buffParam = CLASS(mStateManager.getParamType(detailData.mBuffTypeID)) as CharacterBuffParam;
		if (buffParam == null)
		{
			logError("状态参数创建失败:buffID:" + detailData.mBuffTypeID + ", detailID:" + detailData.mID);
			return null;
		}
		buffParam.registeAllParam();
		if (buffParam.getParamCount() > BUFF_PARAM_COUNT)
		{
			logError("注册的buff参数数量超过了上限");
			return null;
		}
		using var a = new ListScope<string>(out var list);
		list.add(detailData.mParam0);
		list.add(detailData.mParam1);
		list.add(detailData.mParam2);
		list.add(detailData.mParam3);
		list.add(detailData.mParam4);
		list.add(detailData.mParam5);
		list.add(detailData.mParam6);
		list.add(detailData.mParam7);
        buffParam.getParamSet()?.initFromParam(list);
		buffParam.mBuffTime = detailData.mBuffTime;
		buffParam.mBuffData = mExcelBuff.query(detailData.mBuffTypeID);
		buffParam.mBuffDetailData = detailData;
		buffParam.check();
		return buffParam;
	}
}