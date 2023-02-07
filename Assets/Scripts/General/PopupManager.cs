using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SnakeGame
{
    public class PopupManager : MonoBehaviour
    {
        [Inject]
        private IGameFlowManager gameFlowManager;

        [SerializeField]
        private Button gameOverPopupCloseButton;
        [SerializeField]
        private GameObject gameoverPopup;
        [SerializeField]
        private GameObject gameWonPopup;

        private void Start()
        {
            //gameoverPopup.SetActive(false);
            gameWonPopup.SetActive(false);
            gameOverPopupCloseButton.onClick.AddListener(ResetEverything);
            gameFlowManager.GameOver.AddListener(GameOverPopup);
            gameFlowManager.GameClear.AddListener(GameWonPopup);
            gameFlowManager.GameReset.AddListener(DeactivateGameOverPopup);
        }

        private void OnDestroy()
        {
            gameFlowManager.GameOver.RemoveListener(GameOverPopup);
            gameFlowManager.GameClear.RemoveListener(GameWonPopup);
        }

        private void DeactivateGameOverPopup()
        {
            gameoverPopup.SetActive(false);
        }

        private void ResetEverything()
        {
            gameFlowManager.InvokeGameReset();
            gameoverPopup.SetActive(false);
        }

        private void GameOverPopup()
        {
            gameoverPopup.SetActive(true);
        }

        private void GameWonPopup()
        {
            gameWonPopup.SetActive(true);
        }
    }
}