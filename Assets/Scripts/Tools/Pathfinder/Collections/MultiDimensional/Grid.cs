using System;
using System.Collections.Generic;

namespace AOP.Pathfinding.Collections.MultiDimensional
{
    public class Grid<T> : IModelAGrid<T>
    {
        private readonly T[] _grid;
        public Grid(int height, int width)
        {
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            Height = height;
            Width = width;

            _grid = new T[height * width];
        }

        public int Height { get; }

        public int Width { get; }

        public IEnumerable<Coordinate> GetSuccessorPositions(Coordinate node, bool optionsUseDiagonals = false)
        {
            var offsets = GridOffsets.GetOffsets(optionsUseDiagonals);
            foreach (var neighbourOffset in offsets)
            {
                var successorRow = node.x + neighbourOffset.row;

                if (successorRow < 0 || successorRow >= Height)
                {
                    continue;

                }

                var successorColumn = node.y + neighbourOffset.column;

                if (successorColumn < 0 || successorColumn >= Width)
                {
                    continue;
                }

                yield return new Coordinate(successorRow, successorColumn);
            }
        }

        
        public T this[Coordinate position]
        {
            get
            {
                return _grid[ConvertRowColumnToIndex(position.x, position.y)];
            }
            set
            {
                _grid[ConvertRowColumnToIndex(position.x, position.y)] = value;
            }
        }
        public T this[int row, int column]
        {
            get
            {
                return _grid[ConvertRowColumnToIndex(row, column)];
            }
            set
            {
                _grid[ConvertRowColumnToIndex(row, column)] = value;
            }
        }

        private int ConvertRowColumnToIndex(int row, int column)
        {
            return Width * row + column;
        }
    }
}
