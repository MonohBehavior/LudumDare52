using UnityEngine;
using UnityEngine.Events;

namespace SnakeGame
{
    public interface IGridSystem
    {
        UnityEvent SubscriberCollected { get; set; }
        UnityEvent SubscriberNotCollected { get; set; }

        void SetCurrentSubscriberPosition(Int2 gridPos);
        Vector2 InitiatePlayerPosition();
        Vector2 InitiateRandomSubscriberPosition(out Int2 subGridPos);
        Vector2 GetPlayerPosition(WalkingDirection walkingDirection);
        Vector2 GetPlayerPreviousPosition(out Int2 subGridPos);
        Vector2 GetPlayerCurrentPosition();
        bool CheckPlayerColliding(Int2 gridPos);
    }
}