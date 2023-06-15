namespace MazeRunning.Utils.Collections
{
    public static class NumberExtensions
    {
        public static int GetInt(this bool value)
        {
            return value ? 1 : 0;
        }
        public static int GetReverseInt(this bool value)
        {
            return GetInt(!value);
        }
    }
}