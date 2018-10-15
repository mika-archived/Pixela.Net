using System;

namespace Pixela.Enums
{
    public enum GraphType
    {
        Int,

        Float
    }

    public static class GraphTypeExtension
    {
        public static string AsString(this GraphType obj)
        {
            switch (obj)
            {
                case GraphType.Int:
                    return "int";

                case GraphType.Float:
                    return "float";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}