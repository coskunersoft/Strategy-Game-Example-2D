using System.Runtime.InteropServices;

namespace AOP.Pathfinding.Collections.PathFinder
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal readonly struct PathFinderNode
    {
        public Coordinate Position { get; }
        public int G { get; }
        public int H { get; }
        public Coordinate ParentNodePosition { get; }
        public int F { get; }
        public bool HasBeenVisited => F > 0;

        public PathFinderNode(Coordinate position, int g, int h, Coordinate parentNodePosition)
        {
            Position = position;
            G = g;
            H = h;
            ParentNodePosition = parentNodePosition;
            
            F = g + h;
        }
    }
}
