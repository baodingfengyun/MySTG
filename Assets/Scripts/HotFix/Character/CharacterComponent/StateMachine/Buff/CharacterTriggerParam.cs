using System.Collections.Generic;
using static GBR;

// 参数对象
public class CharacterTriggerParam : CharacterBuffParam
{
	public List<int> mBuffDetailIDList = new();	// 可触发的buff列表
	public int mMaxOverlap;						// 最大叠加次数
	public bool mBuffTarget;					// buff附加的目标,false表示给自己附加,true表示给被命中的人附加
	public float mCD;							// 每次触发的CD
	public int mProbability;					// 触发几率,万分比,0表示不会触发,大于等于10000表示百分百触发
	public override void registeAllParam()
	{
		registeParam((string stringParam) => { stringParam.SToIs(mBuffDetailIDList); });
		registeParam((param) => { mMaxOverlap = param.SToI(); });
		registeParam((param) => { mBuffTarget = param.SToI() != 0; });
		registeParam((param) => { mCD = param.SToF(); });
		registeParam((param) => { mProbability = param.SToI(); });
	}
	public override void copy(StateParam otherParam)
	{
		var other = otherParam as CharacterTriggerParam;
		mBuffDetailIDList.AddRange(other.mBuffDetailIDList);
		mMaxOverlap = other.mMaxOverlap;
		mBuffTarget = other.mBuffTarget;
		mCD = other.mCD;
		mProbability = other.mProbability;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelBuffDetail, mBuffDetailIDList);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffDetailIDList.Clear();
		mMaxOverlap = 0;
		mBuffTarget = false;
		mCD = 0.0f;
		mProbability = 0;
	}
};