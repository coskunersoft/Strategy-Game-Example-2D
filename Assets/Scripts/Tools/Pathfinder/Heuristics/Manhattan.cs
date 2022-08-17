using System;

namespace AOP.Pathfinding.Heuristics
{
    public class Manhattan : ICalculateHeuristic
    {
        public int Calculate(Coordinate source, Coordinate destination)
        {
            var heuristicEstimate = 2;
            var h = heuristicEstimate * (Math.Abs(source.x - destination.x) + Math.Abs(source.y - destination.y));
            return h;
        }
    }
}