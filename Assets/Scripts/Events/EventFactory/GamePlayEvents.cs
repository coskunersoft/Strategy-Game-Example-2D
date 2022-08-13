using System;
using AOP.DataCenter;
using AOP.GridSystem;

namespace AOP.EventFactory
{
    public static partial class Events
    {
        public static class GamePlayEvents 
        {
            public static Action<GridCell> OnMouseEnterAnyGridCell;
            public static Action<GridCell> OnMouseExitAnyGridCell;
            public static Action<GridCell> OnAnyGridCellClicked;
            public static Action<BuildingSO> OnAnyBuildDraggedFromMenu;

        }
    }
}