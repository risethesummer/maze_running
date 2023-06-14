using System;
using System.Threading;

namespace MazeRunning.Utils.Task
{
    public class CancellationTokenTracker : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSrc;
        public CancellationToken CancellationToken => _cancellationTokenSrc.Token;
        public void Reset()
        {
            DisposeCancellationTokenSource();
            _cancellationTokenSrc = new CancellationTokenSource();
        }
        private void DisposeCancellationTokenSource()
        {
            _cancellationTokenSrc?.CancelAndDispose();
            _cancellationTokenSrc = null;
        }
        public void Dispose()
        {
            DisposeCancellationTokenSource();
        }
    }
}