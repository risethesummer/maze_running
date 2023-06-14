using System;

namespace MazeRunning.Utils.Collections
{
    public static class CollectionExtensions
    {
        public static void InitArray<T>(this T[,] arr, int width, int height) where T : new()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    arr[i, j] = new T();
                }
            }
        }
    }
}