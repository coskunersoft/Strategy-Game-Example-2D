using System;
using System.Collections.Generic;
using System.Linq;
using AOP.Pathfinding.Heuristics;
using AOP.Pathfinding.Options;
using AOP.GridSystem;
using AOP.Pathfinding.Collections.MultiDimensional;
using AOP.Pathfinding.Collections.PathFinder;

namespace AOP.Pathfinding
{
    public class PathFinder : IFindAPath
    {
        private const int ClosedValue = 0;
        private const int DistanceBetweenNodes = 1;
        private readonly PathFinderOptions _options;
        private readonly WorldGrid _world;
        private readonly ICalculateHeuristic _heuristic;

        public PathFinder(WorldGrid worldGrid, PathFinderOptions pathFinderOptions = null)
        {
            _world = worldGrid ?? throw new ArgumentNullException(nameof(worldGrid));
            _options = pathFinderOptions ?? new PathFinderOptions();
            _heuristic = HeuristicFactory.Create(_options.HeuristicFormula);
        }

        
        public Coordinate[] FindPath(Coordinate start, Coordinate end)
        {
            var nodesVisited = 0;
            IModelAGraph<PathFinderNode> graph = new PathFinderGraph(_world.Height, _world.Width, _options.UseDiagonals);

            var startNode = new PathFinderNode(position: start, g: 0, h: 2, parentNodePosition: start);
            graph.OpenNode(startNode);

            while (graph.HasOpenNodes)
            {
                var q = graph.GetOpenNodeWithSmallestF();

                if (q.Position == end)
                {
                    return OrderClosedNodesAsArray(graph, q);
                }

                if (nodesVisited > _options.SearchLimit)
                {
                    return new Coordinate[0];
                }

                foreach (var successor in graph.GetSuccessors(q))
                {
                    if (_world[successor.Position] == ClosedValue)
                    {
                        continue;
                    }

                    var newG = q.G + DistanceBetweenNodes;

                    if (_options.PunishChangeDirection)
                    {
                        newG += CalculateModifierToG(q, successor, end);
                    }

                    var updatedSuccessor = new PathFinderNode(
                        position: successor.Position,
                        g: newG,
                        h: _heuristic.Calculate(successor.Position, end),
                        parentNodePosition: q.Position);

                    if (BetterPathToSuccessorFound(updatedSuccessor, successor))
                    {
                        graph.OpenNode(updatedSuccessor);
                    }
                }

                nodesVisited++;
            }

            return new Coordinate[0];
        }

        private int CalculateModifierToG(PathFinderNode q, PathFinderNode successor, Coordinate end)
        {
            if (q.Position == q.ParentNodePosition)
            {
                return 0;
            }

            var gPunishment = Math.Abs(successor.Position.x - end.x) + Math.Abs(successor.Position.y - end.y);

            var successorIsVerticallyAdjacentToQ = successor.Position.x - q.Position.x != 0;

            if (successorIsVerticallyAdjacentToQ)
            {
                var qIsVerticallyAdjacentToParent = q.Position.x - q.ParentNodePosition.x == 0;
                if (qIsVerticallyAdjacentToParent)
                {
                    return gPunishment;
                }
            }

            var successorIsHorizontallyAdjacentToQ = successor.Position.x - q.Position.x != 0;

            if (successorIsHorizontallyAdjacentToQ)
            {
                var qIsHorizontallyAdjacentToParent = q.Position.x - q.ParentNodePosition.x == 0;
                if (qIsHorizontallyAdjacentToParent)
                {
                    return gPunishment;
                }
            }

            if (_options.UseDiagonals)
            {
                var successorIsDiagonallyAdjacentToQ = (successor.Position.y - successor.Position.x) == (q.Position.y - q.Position.x);
                if (successorIsDiagonallyAdjacentToQ)
                {
                    var qIsDiagonallyAdjacentToParent = (q.Position.y - q.Position.x) == (q.ParentNodePosition.y - q.ParentNodePosition.x)
                                                        && IsStraightLine(q.ParentNodePosition, q.Position, successor.Position);
                    if (qIsDiagonallyAdjacentToParent)
                    {
                        return gPunishment;
                    }
                }
            }

            return 0;
        }

        private bool IsStraightLine(Coordinate a, Coordinate b, Coordinate c)
        {
            return (a.y * (b.x - c.x) + b.y * (c.x - a.x) + c.y * (a.x - b.x)) / 2 == 0;
        }

        private bool BetterPathToSuccessorFound(PathFinderNode updateSuccessor, PathFinderNode currentSuccessor)
        {
            return !currentSuccessor.HasBeenVisited ||
                (currentSuccessor.HasBeenVisited && updateSuccessor.F < currentSuccessor.F);
        }

        private static Coordinate[] OrderClosedNodesAsArray(IModelAGraph<PathFinderNode> graph, PathFinderNode endNode)
        {
            var path = new Stack<Coordinate>();

            var currentNode = endNode;

            while (currentNode.Position != currentNode.ParentNodePosition)
            {
                path.Push(currentNode.Position);
                currentNode = graph.GetParent(currentNode);
            }

            path.Push(currentNode.Position);

            return path.ToArray();
        }
    }
}