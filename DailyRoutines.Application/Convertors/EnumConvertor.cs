using System;
using System.Linq;

namespace DailyRoutines.Application.Convertors;

public static class EnumConvertor
{
    public static bool EnumContainValue<T>(string value) where T : Enum
    {
        var values = Enum.GetValues(typeof(T)).Cast<T>();

        return values.Any(c => string.Equals(c.ToString(), value, StringComparison.CurrentCultureIgnoreCase));
    }
}