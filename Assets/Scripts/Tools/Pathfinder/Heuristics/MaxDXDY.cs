using System;

namespace AOP.Pathfinding.Heuristics
{
    public class MaxDXDY : ICalculateHeuristic
    {
        public int Calculate(Coordinate source, Coordinate destination)
        {
            var heuristicEstimate = 2;
            var h = heuristicEstimate * (Math.Max(Math.Abs(source.x - destination.x), Math.Abs(source.y - destination.y)));
            return h;
        }
    }
}