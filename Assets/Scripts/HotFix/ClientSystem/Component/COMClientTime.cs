using System;
using static TimeUtility;

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
	public int getTodayEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		DateTime serverZoneNextDayStart = new(serverZoneTime.Year, serverZoneTime.Month, serverZoneTime.Day, hour, 0, 0);
		serverZoneNextDayStart = serverZoneNextDayStart.AddDays(1);
		return (int)(new DateTimeOffset(serverZoneNextDayStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	public int getWeekEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		int dayAdd = serverZoneTime.DayOfWeek == 0 ? 1 : (7 - (int)serverZoneTime.DayOfWeek + 1);
		DateTime serverZoneNextWeekStart = new(serverZoneTime.Year, serverZoneTime.Month, serverZoneTime.Day, hour, 0, 0);
		serverZoneNextWeekStart = serverZoneNextWeekStart.AddDays(dayAdd);
		return (int)(new DateTimeOffset(serverZoneNextWeekStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
	public int getMonthEndRemain(int hour = 0)
	{
		DateTime serverZoneTime = DateTimeOffset.FromUnixTimeSeconds(mServerTimeStampNow).AddSeconds(mServerTimeZoneOffset).UtcDateTime;
		DateTime serverZoneNextMonthStart = new(serverZoneTime.Year, serverZoneTime.Month, 1, hour, 0, 0);
		serverZoneNextMonthStart = serverZoneNextMonthStart.AddMonths(1);
		return (int)(new DateTimeOffset(serverZoneNextMonthStart).ToUnixTimeSeconds() - getNowUTCTimeStamp());
	}
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