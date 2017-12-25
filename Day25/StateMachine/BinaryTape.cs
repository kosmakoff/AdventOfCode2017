using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day25.StateMachine
{
    internal class BinaryTape : IEnumerable<bool>
    {
        private BitArray _data;
        private int _startIndex = 0;

        public BinaryTape()
        {
            _data = new BitArray(8);
        }

        public bool this[int index]
        {
            get => GetItem(index);
            set => SetItem(index, value);
        }

        private bool GetItem(int index)
        {
            if (index < _startIndex || index >= _startIndex + _data.Length)
                return false;

            return _data[index - _startIndex];
        }

        private void SetItem(int index, bool value)
        {
            // resize
            var extendLeftSide = index < _startIndex;
            var extendRightSide = index >= _startIndex + _data.Length;
            if (extendLeftSide || extendRightSide)
            {
                var extent = Math.Max(8, _data.Length / 2);

                var leftSideExtent = extendLeftSide ? extent : 0;
                var rightSideExtent = extendRightSide ? extent : 0;

                var newDataLength = _data.Length + leftSideExtent + rightSideExtent;
                _startIndex -= leftSideExtent;
                var newData = new BitArray(newDataLength);

                for (int i = 0; i < _data.Length; i++)
                {
                    newData[leftSideExtent + i] = _data[i];
                }

                _data = newData;
            }

            _data[index - _startIndex] = value;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return _data.Cast<bool>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
