using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SnakeGame
{
    public class GridSystem : MonoBehaviour, IGridSystem
    {
        public UnityEvent SubscriberCollected { get; set; }
        public UnityEvent SubscriberNotCollected { get; set; }

        public Grid MapGrid;
        public Int2 CurrentGridPosition;
        public Int2 PreviousGridPosition;
        public Int2 SubscriberPosition;

        [Inject]
        private IGameFlowManager gameFlowManager;
        private Vector2 adjustVector = new Vector2(510, 160);

        void Awake()
        {
            SubscriberCollected = new UnityEvent();
            SubscriberNotCollected = new UnityEvent();
            MapGrid = new Grid();
        }

        public Vector2 InitiateRandomSubscriberPosition(out Int2 gridPos)
        {
            var randomGrid = GetRandomGrid();

            while (CurrentGridPosition.Equals(randomGrid))
            {
                randomGrid = GetRandomGrid();
            }

            SetCurrentSubscriberPosition(randomGrid);

            gridPos = randomGrid;

            return ConvertToWorldPosition(randomGrid);
        }

        public Vector2 InitiatePlayerPosition()
        {
            var initGrid = new Int2(MapGrid.XPixels / 2, MapGrid.YPixels / 2);
            CurrentGridPosition = initGrid;
            PreviousGridPosition = CurrentGridPosition;
            return ConvertToWorldPosition(initGrid);
        }

        public Vector2 GetPlayerPosition(WalkingDirection walkingDirection)
        {
            PreviousGridPosition = CurrentGridPosition;

            var nextGridPosition = new Int2();

            switch (walkingDirection)
            {
                case WalkingDirection.Up:
                    nextGridPosition = CurrentGridPosition.Add(new Int2(0, 1));
                    break;

                case WalkingDirection.Down:
                    nextGridPosition = CurrentGridPosition.Add(new Int2(0, -1));
                    break;

                case WalkingDirection.Right:
                    nextGridPosition = CurrentGridPosition.Add(new Int2(1, 0));
                    break;

                case WalkingDirection.Left:
                    nextGridPosition = CurrentGridPosition.Add(new Int2(-1, 0));
                    break;

                default:

                    break;
            }

            CurrentGridPosition = nextGridPosition;

            if (MapGrid.CheckOutsideOfGrid(CurrentGridPosition))
            {
                gameFlowManager.InvokeGameOver();
                return InitiatePlayerPosition();
            }

            if (CurrentGridPosition.Equals(SubscriberPosition))
            {
                SubscriberCollected.Invoke();
            }
            else
            {
                SubscriberNotCollected.Invoke();
            }

            return ConvertToWorldPosition(nextGridPosition);
        }

        public void SetCurrentSubscriberPosition(Int2 gridPos)
        {
            SubscriberPosition = gridPos;
        }

        public Vector2 GetPlayerPreviousPosition(out Int2 gridPos)
        {
            gridPos = PreviousGridPosition;

            return ConvertToWorldPosition(PreviousGridPosition);
        }

        public Vector2 GetPlayerCurrentPosition()
        {
            return ConvertToWorldPosition(CurrentGridPosition);
        }

        public bool CheckPlayerColliding(Int2 gridPos)
        {
            return CurrentGridPosition.Equals(gridPos);
        }

        private Vector2 ConvertToWorldPosition(Int2 gridPosition)
        {
            return new Vector2(MapGrid.PixelSize * gridPosition.x, MapGrid.PixelSize * gridPosition.y) - adjustVector;
        }

        private Int2 GetRandomGrid()
        {
            var randomX = Random.Range(1, MapGrid.XPixels - 1);
            var randomY = Random.Range(1, MapGrid.YPixels - 1);
            return new Int2(randomX, randomY);
        }
    }
}