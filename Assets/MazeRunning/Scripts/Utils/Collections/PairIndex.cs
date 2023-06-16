using System;

namespace MazeRunning.Utils.Collections
{
    public readonly struct PairIndex : IComparable<PairIndex>
    {
        public bool Equals(PairIndex other)
        {
            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            return obj is PairIndex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public int Row { get; init; }
        public int Col { get; init; }
        public int Total => Row + Col;
        public static PairIndex operator +(PairIndex a, PairIndex b) => new()
        {
            Row = a.Row + b.Row,
            Col = a.Col + b.Col
        };
        public static PairIndex operator *(PairIndex a, PairIndex b) => new()
        {
            Row = a.Row * b.Row,
            Col = a.Col * b.Col
        };

        public static bool operator ==(PairIndex a, PairIndex b) => a.Equals(b);

        public static bool operator !=(PairIndex a, PairIndex b) => !(a == b);

        public static PairIndex RandomRange(int width, int height) => new PairIndex
        {
            Row = UnityEngine.Random.Range(0, width),
            Col = UnityEngine.Random.Range(0, height),
        };

        public int CompareTo(PairIndex other)
        {
            var row = Row.CompareTo(other.Row);
            return row != 0 ? row : Col.CompareTo(other.Col);
        }
    }
}