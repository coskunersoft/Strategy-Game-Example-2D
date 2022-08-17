using System;

namespace AOP.Pathfinding.Heuristics
{
    public class Euclidean : ICalculateHeuristic
    {
        public int Calculate(Coordinate source, Coordinate destination)
        {
            var heuristicEstimate = 2;
            var h = (int)(heuristicEstimate * Math.Sqrt(Math.Pow((source.x - destination.x), 2) + Math.Pow((source.y - destination.y), 2)));
            return h;
        }
    }
}