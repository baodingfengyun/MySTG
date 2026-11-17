using System;
using static TimeUtility;

// 客户端"服务器时间"组件:以服务器下发的时间戳为基准,叠加本地流逝时间,推算出持续的服务器时间。
// setData 初始化基准(服务器时间戳+时区偏移+当时本地UTC);update 每帧用本地UTC增量推进 mServerTimeStampNow。
// 提供按服务器时区计算"今日/本周/本月/本年结束剩余秒数"及当月天数,供限时活动/每日重置使用。
public class COMClientTime : GameComponent, IClientSystemComponent
{
	protected int mServerTimeZoneOffset;
	protected int mServerTimeStampNow;
	protected int mServerTimeStampInit;
	protected int mClientTimeStampInit;
	public void clear()
	{
		mServerTimeZoneOffset = 0;
		mServerTimeStampNow = 0;
		mServerTimeStampInit = 0;
		mClientTimeStampInit = 0;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mServerTimeZoneOffset = 0;
		mServerTimeStampNow = 0;
		mServerTimeStampInit = 0;
		mClientTimeStampInit = 0;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		mServerTimeStampNow = mServerTimeStampInit + (int)getNowUTCTimeStamp() - mClientTimeStampInit;
	}
	public void setData(int timeStamp, int timeZoneOffset)
	{
		mServerTimeStampInit = timeStamp;
		mServerTimeStampNow = timeStamp;
		mServerTimeZoneOffset = timeZoneOffset;
		mClientTimeStampInit = (int)getNowUTCTimeStamp();
	}
	// 返回距离服务器时区"下一个指定小时(默认0点)"的剩余秒数,即每日重置倒计时。
	public int getTodayEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		DateTime serverZoneNextDayStart = new(serverZoneTime.Year, serverZoneTime.Month, serverZoneTime.Day, hour, 0, 0);
		serverZoneNextDayStart = serverZoneNextDayStart.AddDays(1);
		return (int)(new DateTimeOffset(serverZoneNextDayStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	// 返回距离服务器时区"下周一指定小时"的剩余秒数,即每周重置倒计时。
	public int getWeekEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		int dayAdd = serverZoneTime.DayOfWeek == 0 ? 1 : (7 - (int)serverZoneTime.DayOfWeek + 1);
		DateTime serverZoneNextWeekStart = new(serverZoneTime.Year, serverZoneTime.Month, serverZoneTime.Day, hour, 0, 0);
		serverZoneNextWeekStart = serverZoneNextWeekStart.AddDays(dayAdd);
		return (int)(new DateTimeOffset(serverZoneNextWeekStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	// 返回距离服务器时区"下月1日指定小时"的剩余秒数,即每月重置倒计时。
	public int getMonthEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		DateTime serverZoneNextMonthStart = new(serverZoneTime.Year, serverZoneTime.Month, 1, hour, 0, 0);
		serverZoneNextMonthStart = serverZoneNextMonthStart.AddMonths(1);
		return (int)(new DateTimeOffset(serverZoneNextMonthStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	// 返回距离服务器时区"次年1月1日指定小时"的剩余秒数,即每年重置倒计时。
	public int getYearEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		DateTime serverZoneNextYearStart = new(serverZoneTime.Year, 1, 1, hour, 0, 0);
		serverZoneNextYearStart = serverZoneNextYearStart.AddYears(1);
		return (int)(new DateTimeOffset(serverZoneNextYearStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	public int getMonthDay()
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		return DateTime.DaysInMonth(serverZoneTime.Year, serverZoneTime.Month);
	}
}