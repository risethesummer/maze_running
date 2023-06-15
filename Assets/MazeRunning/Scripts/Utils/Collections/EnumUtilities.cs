using System;

namespace MazeRunning.Utils.Collections
{
    public static class EnumUtilities
    {
        public static int Count<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T)).Length;
        }
    }
}