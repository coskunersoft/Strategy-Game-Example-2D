using System.Collections.Generic;

namespace AOP.Pathfinding.Collections.PathFinder
{
    internal interface IModelAGraph<T>
    {
        bool HasOpenNodes { get; }
        IEnumerable<T> GetSuccessors(T node);
        T GetParent(T node);
        void OpenNode(T node);
        T GetOpenNodeWithSmallestF();
    }
}