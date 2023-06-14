using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeRunning.Utils.Collections
{
    [Serializable]
    public abstract class TabCollection<T> : IReadOnlyCollection<T>
    {
        [SerializeField] private T[] items;
        public int CurrentIndex { get; private set; } = 0;
        public T GetCurrentItem() => items[CurrentIndex];
        public abstract int GetNextIndex();
        public T Tab()
        {
            CurrentIndex = GetNextIndex();
            return GetCurrentItem();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return items.AsEnumerable().GetEnumerator();;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public int Count => items.Length;
    }
}