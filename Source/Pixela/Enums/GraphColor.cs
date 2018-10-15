using System;

namespace Pixela.Enums
{
    public enum GraphColor
    {
        Shibafu,

        Momiji,

        Sora,

        Ichou,

        Ajisai,

        Kuro
    }

    public static class GraphColorExtensions
    {
        public static string AsString(this GraphColor obj)
        {
            switch (obj)
            {
                case GraphColor.Shibafu:
                    return "shibafu";

                case GraphColor.Momiji:
                    return "momiji";

                case GraphColor.Sora:
                    return "sora";

                case GraphColor.Ichou:
                    return "ichou";

                case GraphColor.Ajisai:
                    return "ajisai";

                case GraphColor.Kuro:
                    return "kuro";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}