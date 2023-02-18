using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private Button gameOverPopupCloseButton;
        [SerializeField]
        private GameObject gameoverPopup;

        private void Start()
        {
            gameOverPopupCloseButton.onClick.AddListener(ResetGame);
            GameFlowEvents.GameOver.AddListener(ActivateGameOverPopup);
            GameFlowEvents.GameReset.AddListener(DeactivateGameOverPopup);
        }

        private void OnDestroy()
        {
            GameFlowEvents.GameOver.RemoveListener(ActivateGameOverPopup);
            GameFlowEvents.GameReset.RemoveListener(DeactivateGameOverPopup);
        }

        private void ResetGame()
        {
            GameFlowEvents.InvokeGameReset();
            DeactivateGameOverPopup();
        }

        private void DeactivateGameOverPopup()
        {
            gameoverPopup.SetActive(false);
        }

        private void ActivateGameOverPopup()
        {
            gameoverPopup.SetActive(true);
        }
    }
}