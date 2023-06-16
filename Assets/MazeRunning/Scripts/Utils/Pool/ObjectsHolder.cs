using System;
using System.Collections.Generic;

namespace MazeRunning.Utils.Pool
{
    public class ObjectsHolder
    {
        private readonly Queue<IDisposable> _disposables = new();
        public void Hold(IDisposable disposable)
        {
            _disposables.Enqueue(disposable);
        }
        public void ReleaseAll()
        {
            while (_disposables.Count > 0)
            {
                var disposable = _disposables.Dequeue();
                disposable.Dispose();
            }
        }
    }
}