using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SnakeGame
{
    public class FillSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider progressSlider;
        [Inject]
        private IGameFlowManager gameFlowManager;

        void Start()
        {
            gameFlowManager.GameOver.AddListener(FillFullSlider);
            gameFlowManager.GameReset.AddListener(EmptySlider);
        }

        private void OnDestroy()
        {
            gameFlowManager.GameOver.RemoveListener(FillFullSlider);
            gameFlowManager.GameOver.RemoveListener(EmptySlider);
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