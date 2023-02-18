using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class FillSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider progressSlider;

        void Start()
        {
            GameFlowEvents.GameOver.AddListener(FillFullSlider);
            GameFlowEvents.GameReset.AddListener(EmptySlider);
        }

        private void OnDestroy()
        {
            GameFlowEvents.GameOver.RemoveListener(FillFullSlider);
            GameFlowEvents.GameReset.RemoveListener(EmptySlider);
        }

        private void FillFullSlider()
        {
            progressSlider.value = 1;
        }

        private void EmptySlider()
        {
            progressSlider.value = 0.3f;
        }
    }
}