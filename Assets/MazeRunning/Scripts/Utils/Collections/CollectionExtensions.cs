using System;
using System.Collections.Generic;

namespace MazeRunning.Utils.Collections
{
    public static class CollectionExtensions
    {
        public static void Init2DArray<T>(this T[,] arr, int width, int height) where T : new()
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    arr[i, j] = new T();
                }
            }
        }

        public static T RandomElement<T>(this T[] arr)
        {
            var randIndex = UnityEngine.Random.Range(0, arr.Length);
            return arr[randIndex];
        }
        public static T RandomElement<T>(this IReadOnlyList<T> list)
        {
            var randIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randIndex];
        }
        public static T RandomElement<T>(this Span<T> arr)
        {
            var randIndex = UnityEngine.Random.Range(0, arr.Length);
            return arr[randIndex];
        }
    }
}