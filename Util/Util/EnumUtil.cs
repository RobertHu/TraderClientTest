using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public static class EnumUtil
    {
        public static T ConvertEnumStringToEnum<T>(string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target can not be empty or null");
            }
            return (T)Enum.Parse(typeof(T), target);
        }
    }
}
