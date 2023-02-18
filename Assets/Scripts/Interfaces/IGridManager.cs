using UnityEngine;

namespace SnakeGame
{
    public interface IGridManager
    {
        void SetCurrentSubscriberCoordination(Int2 gridPos);
        Vector2 InitiatePlayerCoordination();
        Vector2 InitiateRandomSubscriberPosition(out Int2 subGridPos);
        Vector2 GetPlayerPosition(WalkingDirection walkingDirection);
        Vector2 GetPlayerPreviousPosition(out Int2 subGridPos);
        bool CheckIfPlayerOnCoordinate(Int2 gridPos);
    }
}