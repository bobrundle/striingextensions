using System;
using System.Reflection;

namespace StringSupport
{
    public static class StringExtensions
    {
        public static T GetValue<T>(this string s)
        {
            MethodInfo mi = typeof(T).GetMethod("Parse", new Type[] { typeof(string) });
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            else if (mi != null)
            {
                return (T)mi.Invoke(typeof(T), new object[] { s });
            }
            else if (typeof(T).IsEnum)
            {
                if(Enum.TryParse(typeof(T), s, out object ev))
                    return (T)(object)ev;
                else
                    throw new ArgumentException($"{s} is not a valid member of {typeof(T).Name}");
            }
            else
            {
                throw new ArgumentException($"No conversion supported for {typeof(T).Name}");
            }
        }
        public static T GetValue<T>(this string s, T defaultValue)
        {
            MethodInfo mi = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });
            if (s == null)
            {
                return defaultValue;
            }
            else if (mi != null)
            {
                T v;
                var p = new object[] { s, null };
                if ((bool)mi.Invoke(null, p))
                    return (T)p[1];
                else
                    return defaultValue;
            }
            else if (typeof(T).IsEnum && Enum.TryParse(typeof(T), s, out object ev))
            {
                return (T)ev;
            }
            else
            {
                return defaultValue;
            }
        }
        public static string SetValue(object value)
        {
            if (value == null)
            {
                return null;
            }
            else if (value is float)
            {
                return ((float)value).ToString("R");
            }
            else if (value is double)
            {
                return ((double)value).ToString("R");
            }
            else if (value is DateTime)
            {
                return ((DateTime)value).ToString("O");
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
