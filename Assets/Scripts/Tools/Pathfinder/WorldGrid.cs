using AOP.Pathfinding.Collections.MultiDimensional;

namespace AOP.Pathfinding
{
    public class WorldGrid : Grid<int>
    {
        public WorldGrid(int height, int width) : base(height, width)
        {
        }

      
        public WorldGrid(int[,] worldArray) : base(worldArray.GetLength(0), worldArray.GetLength(1))
        {
            for (var row = 0; row < worldArray.GetLength(0); row++)
            {
                for (var column = 0; column < worldArray.GetLength(1); column++)
                {
                    this[row, column] = worldArray[row, column];
                }
            }
        }
    }
}