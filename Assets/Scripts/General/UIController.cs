using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

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

        [SerializeField]
        private Text TimeStamp;

        [Inject]
        private IGameFlowManager gameFlowManager;

        private WaitForSeconds waitForASecond;

        void Start()
        {
            PlayButton.onClick.AddListener(Play);
            PauseButton.onClick.AddListener(Pause);
            ReplayButton.onClick.AddListener(Replay);
            PlayButton.gameObject.SetActive(false);
            PauseField.SetActive(false);
            gameFlowManager.GameOver.AddListener(ActivateReplay);
            gameFlowManager.GameClear.AddListener(ActivateReplay);
            gameFlowManager.GameReset.AddListener(Play);

            // StartCoroutine(UpdateTimeStamp());

            ActivateReplay();
            
            waitForASecond = new WaitForSeconds(1f);
        }

        private void OnDestroy()
        {
            gameFlowManager.GameOver.RemoveListener(Pause);
            gameFlowManager.GameClear.RemoveListener(Pause);
            gameFlowManager.GameReset.RemoveListener(Play);
        }

        //private IEnumerator UpdateTimeStamp()
        //{
        //    // TODO: for staging
        //    var time = Time.timeSinceLevelLoad;
        //    TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        //    TimeSpan timeLimit = TimeSpan.FromMinutes(5);

        //    while (true)
        //    //while (timeSpan < timeLimit)
        //    {
        //        time = Time.timeSinceLevelLoad;
        //        timeSpan = TimeSpan.FromSeconds(time);
        //        var timeString = timeSpan.ToString(@"mm\:ss");
        //        TimeStamp.text = $"{timeString} / 5:00";

        //        yield return waitForASecond;
        //    }

        //    gameFlowManager.InvokeGameOver();
        //}

        private void Play()
        {
            PauseButton.gameObject.SetActive(true);

            ReplayButton.gameObject.SetActive(false);
            PauseField.gameObject.SetActive(false);
            Time.timeScale = 1f;

            PlayButton.gameObject.SetActive(false);
        }

        private void ActivateReplay()
        {
            ReplayButton.gameObject.SetActive(true);
            PauseField.gameObject.SetActive(true);

            PauseButton.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }

        private void Replay()
        {
            gameFlowManager.InvokeGameReset();
            Play();
        }

        private void Pause()
        {
            PlayButton.gameObject.SetActive(true);

            PauseField.gameObject.SetActive(true);
            Time.timeScale = 0f;

            ReplayButton.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }
    }
}