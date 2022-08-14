using System;
using AOP.DataCenter;
using AOP.GridSystem;
using AOP.GamePlay.Units;

namespace AOP.EventFactory
{
    public static partial class Events
    {
        public static class GamePlayEvents 
        {
            public static Action<IGameBuildingUnit> OnAnyBuildigPlaced;
            public static Action<IGameUnit> OnAnyUnitSelectedInGameArea;

            public static Action<GridCell> OnMouseEnterAnyGridCell;
            public static Action<GridCell> OnMouseExitAnyGridCell;
            public static Action<GridCell> OnAnyGridCellClicked;

            public static Action<BuildingSO> OnAnyBuildDraggedFromMenu;

        }
    }
}