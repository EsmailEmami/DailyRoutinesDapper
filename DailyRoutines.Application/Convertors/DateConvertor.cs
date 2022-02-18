using DailyRoutines.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DailyRoutines.Application.Convertors;

public static class DateConvertor
{
    public static string ToPersianDateTime(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetHour(value).ToString("00") + ":" + pc.GetMinute(value).ToString("00") + " - " +
               pc.GetYear(value).ToString("00") + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }

    public static string ToPersianDate(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetYear(value).ToString("00") + "/" + pc.GetMonth(value).ToString("00") + "/" +
        pc.GetDayOfMonth(value).ToString("00");
    }

    public static int ToPersianYear(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetYear(value);
    }

    public static int ToPersianMonth(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetMonth(value);
    }

    public static int ToPersianDay(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetDayOfMonth(value);
    }

    public static DateTime ToGeorgianDate(this string dateTime,string spliter)
    {
        string englishDateTime = dateTime.PersianNumberToLatinNumber();
        
        string[] date = dateTime.Split(spliter);

        return new DateTime(int.Parse(date[0]),
            int.Parse(date[1]),
            int.Parse(date[2]),
            new PersianCalendar());
    }

    public static DateTime ToGeorgianDate(int year, int month, int day)
    {
        return new DateTime(year, month, day, new PersianCalendar());
    }

    public static DateTime ToGeorgianDateTime(this string dateTime)
    {
        string englishDateTime = dateTime.PersianNumberToLatinNumber();

        string[] dateTimeSplit = englishDateTime.Split(" ");
        string[] time = dateTimeSplit[0].Split(":");
        string[] date = dateTimeSplit[1].Split("/");

        return new DateTime(int.Parse(date[0]),
            int.Parse(date[1]),
            int.Parse(date[2]),
            int.Parse(time[0]),
            int.Parse(time[1]),
            0,
            new PersianCalendar());
    }

    public static int ToGeorgianYear(this int year)
    {
        return new DateTime(year, 1, 1, new PersianCalendar()).Year;
    }

    public static string ToPersianMonthString(this int month)
    {
        Dictionary<int, string> lettersDictionary = new Dictionary<int, string>
        {
            [1] = "فروردین",
            [2] = "اردیبهشت",
            [3] = "خرداد",
            [4] = "نیر",
            [5] = "مرداد",
            [6] = "شهریور",
            [7] = "مهر",
            [8] = "آبان",
            [9] = "آذر",
            [10] = "دی",
            [11] = "بهمن",
            [12] = "اسفند"
        };


        return lettersDictionary[month];
    }
}
