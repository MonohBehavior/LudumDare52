using UnityEngine.Events;

namespace SnakeGame
{
    public interface IGameFlowManager
    {
        UnityEvent GameOver { get; set; }
        UnityEvent GameReset { get; set; }
        UnityEvent GameClear { get; set; }

        void InvokeGameOver();
        void InvokeGameReset();
        void InvokeGameClear();
    }
}