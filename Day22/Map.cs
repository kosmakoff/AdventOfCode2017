using System;

namespace Day22
{
    internal class Map<T>
    {
        private T[] _data;

        private int _totalRows;
        private int _totalColumns;
        private int _topmostRow;
        private int _leftMostColumn;

        public Map(int rows, int columns, T[] data)
        {
            if (rows * columns != data.Length)
                throw new ArgumentException("Data length must match dimensions");

            _topmostRow = 0;
            _leftMostColumn = 0;
            _totalRows = rows;
            _totalColumns = columns;
            _data = data;
        }

        public T this[int row, int column]
        {
            get => GetBit(row, column);
            set => SetBit(row, column, value);
        }

        private T GetBit(int row, int column)
        {
            if (row < _topmostRow || row >= _topmostRow + _totalRows ||
                column < _leftMostColumn || column >= _leftMostColumn + _totalColumns)
                return default(T);

            var indexRow = row - _topmostRow;
            var indexColumn = column - _leftMostColumn;
            var index = indexRow * _totalColumns + indexColumn;

            return _data[index];
        }

        private void SetBit(int row, int column, T value)
        {
            bool expandLeft = column < _leftMostColumn;
            bool expandTop = row < _topmostRow;
            bool expandRight = column >= _leftMostColumn + _totalColumns;
            bool expandBottom = row >= _topmostRow + _totalRows;

            if (expandLeft || expandTop || expandRight || expandBottom)
            {
                int expandLeftSize = expandLeft ? Math.Max(1, _totalColumns / 2) : 0;
                int expandTopSize = expandTop ? Math.Max(1, _totalRows / 2) : 0;
                int expandRightSize = expandRight ? Math.Max(1, _totalColumns / 2) : 0;
                int expandBottomSize = expandBottom ? Math.Max(1, _totalRows / 2) : 0;

                ExpandDataField(expandLeftSize, expandTopSize, expandRightSize, expandBottomSize);
            }

            var indexRow = row - _topmostRow;
            var indexColumn = column - _leftMostColumn;
            var index = indexRow * _totalColumns + indexColumn;

            _data[index] = value;
        }

        private void ExpandDataField(int expandLeftSize, int expandTopSize, int expandRightSize, int expandBottomSize)
        {
            var newLeftmostColumn = _leftMostColumn - expandLeftSize;
            var newTopmostRow = _topmostRow - expandTopSize;
            var newTotalColumns = _totalColumns + expandLeftSize + expandRightSize;
            var newTotalRows = _totalRows + expandTopSize + expandBottomSize;

            var newData = new T[newTotalRows * newTotalColumns];

            for (int r = 0; r < _totalRows; r++)
            for (int c = 0; c < _totalColumns; c++)
            {
                var oldDataIndex = r * _totalColumns + c;
                var newDataIndex = (r + expandTopSize) * newTotalColumns + (c + expandLeftSize);
                newData[newDataIndex] = _data[oldDataIndex];
            }

            _leftMostColumn = newLeftmostColumn;
            _topmostRow = newTopmostRow;
            _totalColumns = newTotalColumns;
            _totalRows = newTotalRows;

            _data = newData;
        }

        public void DebugPrint(Func<T, char> formatter)
        {
            for (int r = 0; r < _totalRows; r++)
            {
                for (int c = 0; c < _totalColumns; c++)
                {
                    var index = r * _totalColumns + c;
                    Console.Write(formatter(_data[index]));
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
