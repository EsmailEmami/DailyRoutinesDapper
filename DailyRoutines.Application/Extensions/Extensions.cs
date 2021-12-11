using System;

namespace DailyRoutines.Application.Extensions
{
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
    }
}