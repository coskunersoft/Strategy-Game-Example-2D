using UnityEngine;

namespace AOP.Pathfinding.Heuristics
{
    public interface ICalculateHeuristic
    {
        int Calculate(Coordinate source, Coordinate destination);
    }
}