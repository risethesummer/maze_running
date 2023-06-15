using MazeRunning.SharedStructures.Data;

namespace MazeRunning.SharedStructures.Signals
{
    public readonly struct EndLevelSignal
    {
        public PlayerType Winner { get; init; } 
    }
}