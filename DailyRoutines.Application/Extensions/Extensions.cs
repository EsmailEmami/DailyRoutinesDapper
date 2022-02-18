using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyRoutines.Application.Extensions;

public static class Extensions
{
    public static Guid ToGuid(this string value)
    {
        try
        {
            return Guid.Parse(value);
        }
        catch
        {
            return Guid.Empty;
        }
    }

    public static bool IsEmpty(this Guid value) => value == Guid.Empty;

    public static string Fixed(this string value) =>
        value.Trim().ToLower();

    public static string PersianNumberToLatinNumber(this string text)
    {
        Dictionary<string, string> lettersDictionary = new Dictionary<string, string>
        {
            ["۰"] = "0",
            ["۱"] = "1",
            ["۲"] = "2",
            ["۳"] = "3",
            ["۴"] = "4",
            ["۵"] = "5",
            ["۶"] = "6",
            ["۷"] = "7",
            ["۸"] = "8",
            ["۹"] = "9"
        };
        return lettersDictionary.Aggregate(text, (current, item) =>
            current.Replace(item.Key, item.Value));
    }
}