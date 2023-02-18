using UnityEngine;

namespace SnakeGame
{
    public class GridManager : MonoBehaviour, IGridManager
    {
        private Grid mapGrid = new Grid();
        private Int2 currentPlayerGridCoordination;
        private Int2 previousPlayerGridCoordination;
        private Int2 subscriberCoordination;

        private Vector2 adjustVector = new Vector2(510, 160);

        public Vector2 InitiateRandomSubscriberPosition(out Int2 gridCoordination)
        {
            var randomCoordination = GetRandomCoordinateOnGrid();

            while (currentPlayerGridCoordination.Equals(randomCoordination))
            {
                randomCoordination = GetRandomCoordinateOnGrid();
            }

            SetCurrentSubscriberCoordination(randomCoordination);

            gridCoordination = randomCoordination;

            return ConvertToWorldPosition(randomCoordination);
        }

        public Vector2 InitiatePlayerCoordination()
        {
            var playerStartingCoordination = new Int2(mapGrid.XPixels / 2, mapGrid.YPixels / 2);
            previousPlayerGridCoordination = currentPlayerGridCoordination = playerStartingCoordination;

            return ConvertToWorldPosition(playerStartingCoordination);
        }

        public Vector2 GetPlayerPosition(WalkingDirection walkingDirection)
        {
            previousPlayerGridCoordination = currentPlayerGridCoordination;

            var nextGridRelativeCoordination = new Int2();

            switch (walkingDirection)
            {
                case WalkingDirection.Up:
                    nextGridRelativeCoordination = currentPlayerGridCoordination.Add(new Int2(0, 1));
                    break;

                case WalkingDirection.Down:
                    nextGridRelativeCoordination = currentPlayerGridCoordination.Add(new Int2(0, -1));
                    break;

                case WalkingDirection.Right:
                    nextGridRelativeCoordination = currentPlayerGridCoordination.Add(new Int2(1, 0));
                    break;

                case WalkingDirection.Left:
                    nextGridRelativeCoordination = currentPlayerGridCoordination.Add(new Int2(-1, 0));
                    break;

                default:

                    break;
            }

            currentPlayerGridCoordination = nextGridRelativeCoordination;

            if (mapGrid.CheckOutsideOfGrid(currentPlayerGridCoordination))
            {
                GameFlowEvents.InvokeGameOver();
                return InitiatePlayerCoordination();
            }
            else if (currentPlayerGridCoordination.Equals(subscriberCoordination))
            {
                GameFlowEvents.SubscriberCollected.Invoke();
            }
            else
            {
                GameFlowEvents.SubscriberNotCollected.Invoke();
            }

            return ConvertToWorldPosition(nextGridRelativeCoordination);
        }

        public void SetCurrentSubscriberCoordination(Int2 gridPos)
        {
            subscriberCoordination = gridPos;
        }

        public Vector2 GetPlayerPreviousPosition(out Int2 gridPos)
        {
            gridPos = previousPlayerGridCoordination;

            return ConvertToWorldPosition(previousPlayerGridCoordination);
        }

        public bool CheckIfPlayerOnCoordinate(Int2 gridPos)
        {
            return currentPlayerGridCoordination.Equals(gridPos);
        }

        private Vector2 ConvertToWorldPosition(Int2 gridPosition)
        {
            return new Vector2
                (mapGrid.PixelSize * gridPosition.x,
                mapGrid.PixelSize * gridPosition.y)
                - adjustVector;
        }

        private Int2 GetRandomCoordinateOnGrid()
        {
            var randomX = Random.Range(1, mapGrid.XPixels - 1);
            var randomY = Random.Range(1, mapGrid.YPixels - 1);
            return new Int2(randomX, randomY);
        }
    }
}