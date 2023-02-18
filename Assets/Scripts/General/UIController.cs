using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Button PlayButton;
        [SerializeField]
        private Button PauseButton;
        [SerializeField]
        private Button ReplayButton;
        [SerializeField]
        private GameObject PauseField;

        void Start()
        {
            PlayButton.onClick.AddListener(OnPlayButton);
            PauseButton.onClick.AddListener(OnPauseButton);
            ReplayButton.onClick.AddListener(OnReplayButton);
            GameFlowEvents.GameOver.AddListener(OnGameOver);
            GameFlowEvents.GameClear.AddListener(OnGameOver);
            GameFlowEvents.GameReset.AddListener(OnPlayButton);

            PlayButton.gameObject.SetActive(false);
            PauseField.SetActive(false);

            OnGameOver();
        }

        private void OnDestroy()
        {
            GameFlowEvents.GameOver.RemoveListener(OnPauseButton);
            GameFlowEvents.GameClear.RemoveListener(OnPauseButton);
            GameFlowEvents.GameReset.RemoveListener(OnPlayButton);
        }

        private void OnReplayButton()
        {
            GameFlowEvents.InvokeGameReset();
            OnPlayButton();
        }

        private void OnPlayButton()
        {
            ChangeStatus(PlayStatus.Play, 1f);
        }

        private void OnGameOver()
        {
            ChangeStatus(PlayStatus.Replay, 0f);
        }

        private void OnPauseButton()
        {
            ChangeStatus(PlayStatus.Pause, 0f);
        }

        private void ChangeStatus(PlayStatus status, float timeScale)
        {
            PauseButton.gameObject.SetActive(status == PlayStatus.Play);
            PauseField.gameObject.SetActive(status != PlayStatus.Play);

            ReplayButton.gameObject.SetActive(status == PlayStatus.Replay);
            PlayButton.gameObject.SetActive(status == PlayStatus.Pause);

            Time.timeScale = timeScale;
        }
    }
}