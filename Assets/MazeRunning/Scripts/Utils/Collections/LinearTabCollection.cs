using System;

namespace MazeRunning.Utils.Collections
{
    [Serializable]
    public class LinearTabCollection<T> : TabCollection<T>
    {
        private bool _leftToRight = true;
        public override int GetNextIndex() //=> (CurrentIndex + 1) % Count; //Ring
        {
            var offset = _leftToRight ? 1 : -1;
            var newIndex = CurrentIndex + offset;
            //If reaching the first or the last element => revert direction
            if (newIndex == 0 || newIndex == Count - 1)
                _leftToRight = !_leftToRight;
            return newIndex;
        }
    }
}