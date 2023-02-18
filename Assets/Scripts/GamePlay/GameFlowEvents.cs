using UnityEngine.Events;

namespace SnakeGame
{
    public static class GameFlowEvents
    {
        public static UnityEvent GameOver = new UnityEvent();
        public static UnityEvent GameReset = new UnityEvent();
        public static UnityEvent GameClear = new UnityEvent();
        public static UnityEvent SubscriberCollected = new UnityEvent();
        public static UnityEvent SubscriberNotCollected = new UnityEvent();

        public static void InvokeGameOver()
        {
            GameOver.Invoke();
        }

        public static void InvokeGameReset()
        {
            GameReset.Invoke();
        }

        public static void InvokeGameClear()
        {
            GameClear.Invoke();
        }

        public static void InvokeSubscriberCollected()
        {
            SubscriberCollected.Invoke();
        }

        public static void InvokeSubscriberNotCollected()
        {
            SubscriberNotCollected.Invoke();
        }
    }
}