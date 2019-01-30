using System;
using System.Collections.Generic;
using System.Text;

namespace Pixela.Enums
{
    public enum SufficientType
    {
        Increment,

        Decrement,

        None
    }

    public static class SufficientTypeExtension
    {
        public static string AsString(this SufficientType obj)
        {
            switch (obj)
            {
                case SufficientType.Increment:
                    return "increment";

                case SufficientType.Decrement:
                    return "decrement";

                case SufficientType.None:
                    return "none";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}