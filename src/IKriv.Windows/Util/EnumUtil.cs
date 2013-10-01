using System;

namespace IKriv.Windows.Util
{
    public static class EnumUtil
    {
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static T? TryParse<T>(string s) where T:struct
        {
            T result;
            if (!Enum.TryParse(s, out result)) return null;
            return result;
        }
    }
}
