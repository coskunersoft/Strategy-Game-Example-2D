using System;

namespace AOP.Pathfinding.Heuristics
{
    public class EuclideanNoSQR : ICalculateHeuristic
    {
        public int Calculate(Coordinate source, Coordinate destination)
        {
            var heuristicEstimate = 2;
            var h = (int)(heuristicEstimate * (Math.Pow((source.x - destination.x), 2) + Math.Pow((source.y - destination.y), 2)));
            return h;
        }
    }
}