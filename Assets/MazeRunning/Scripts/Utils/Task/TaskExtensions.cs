using System.Threading;

namespace MazeRunning.Utils.Task
{
    public static class TaskExtensions
    {

        public static void CancelAndDispose(this CancellationTokenSource source)
        {
            source.Cancel();
            source.Dispose();
        }
    }
}