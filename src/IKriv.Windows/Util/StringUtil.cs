using System;

namespace IKriv.Windows.Util
{
    static class StringUtil
    {
        static string SafeToString(this object obj)
        {
            if (obj == null) return String.Empty;
            return obj.ToString();
        }
    }
}
