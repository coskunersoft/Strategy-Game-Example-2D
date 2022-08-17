using System.Collections.Generic;

namespace AOP.Pathfinding.Collections.MultiDimensional
{
    public interface IModelAGrid<T>
    {
        int Height { get; }
        int Width { get; }
        T this[int row, int column] { get; set; }
        T this[Coordinate position] { get; set; }
        IEnumerable<Coordinate> GetSuccessorPositions(Coordinate node, bool optionsUseDiagonals = false);
    }
}