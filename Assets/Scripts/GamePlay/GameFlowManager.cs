using UnityEngine;
using UnityEngine.Events;

namespace SnakeGame
{
    public class GameFlowManager : MonoBehaviour, IGameFlowManager
    {
        public UnityEvent GameOver { get; set; }
        public UnityEvent GameReset { get; set; }
        public UnityEvent GameClear { get; set; }

        private void Awake()
        {
            GameOver = new UnityEvent();
            GameReset = new UnityEvent();
            GameClear = new UnityEvent();
        }

        public void InvokeGameOver()
        {
            GameOver.Invoke();
        }

        public void InvokeGameReset()
        {
            GameReset.Invoke();
        }

        public void InvokeGameClear()
        {
            GameClear.Invoke();
        }
    }
}