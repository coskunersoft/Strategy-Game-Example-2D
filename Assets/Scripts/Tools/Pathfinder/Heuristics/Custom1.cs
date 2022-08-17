using System;

namespace AOP.Pathfinding.Heuristics
{
    public class Custom1 : ICalculateHeuristic
    {
        public int Calculate(Coordinate source, Coordinate destination)
        {
            var heuristicEstimate = 2;
            var dxy = new Coordinate(Math.Abs(destination.x - source.x), Math.Abs(destination.y - source.y));
            var Orthogonal = Math.Abs(dxy.x - dxy.y);
            var Diagonal = Math.Abs(((dxy.x + dxy.y) - Orthogonal) / 2);
            var h = heuristicEstimate * (Diagonal + Orthogonal + dxy.x + dxy.y);
            return h;
        }
    }
}