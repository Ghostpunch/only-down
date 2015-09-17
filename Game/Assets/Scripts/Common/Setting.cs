using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghostpunch.OnlyDown.Common
{
    public interface ISetting<T>
    {
        T Value { get; set; }
    }

    public class Setting<T> : ISetting<T>
    {
        public T Value { get; set; }
    }
}
