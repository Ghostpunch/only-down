using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Utilities
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value)
        {
            Type type = value.GetType();
            return Enum.GetName(type, value);
        }
    }
}
