using System;
using AOP.DataCenter;
using AOP.GridSystem;
using AOP.GamePlay.Units;
using AOP.GamePlay.DataMaps;

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
            public static Action<GridCell> OnAnyGridCellMouseOneClicked;
            public static Action<GridCell> OnAnyGridCellMouseTwoClicked;


            public static Action<BuildingSO> OnAnyBuildDraggedFromMenu;

            public static Action<IGameBarrackBuildingUnit,MilitaryUnitSO> OnAnyBarrackProductionCreateRequest;
            public static Action<IGameBarrackBuildingUnit, MilitaryUnitSO> OnAnyBarrackBuildingStartedProducting;
            public static Action<IGameBarrackBuildingUnit, MilitaryUnitSO> OnAnyBarrackBuildingProductingProgress;
            public static Action<IGameBarrackBuildingUnit, MilitaryUnitSO> OnAnyBarrackBuildingCancelProducting;
            public static Action<IGameBarrackBuildingUnit,IGameMilitaryUnit> OnAnyBarrackBuildingFinishProducting;
        }
    }
}