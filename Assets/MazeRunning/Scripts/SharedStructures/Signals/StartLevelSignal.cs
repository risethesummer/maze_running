using System.Collections.Generic;
using MazeRunning.SharedStructures.Data;

namespace MazeRunning.SharedStructures.Signals
{
    public readonly struct StartLevelSignal
    {
        public IEnumerable<KeyValuePair<PlayerType, int>> Scores { get; init; }
        public int CurrentLevel { get; init; }
    }
}