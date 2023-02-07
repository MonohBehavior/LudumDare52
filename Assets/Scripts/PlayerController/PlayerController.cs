using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SnakeGame
{
    public enum WalkingDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public class PlayerController : MonoBehaviour
    {
        public Animator UnityAnim;
        public WalkingDirection CurrentDirection;
        public WaitForSeconds WaitForOneSecond;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] audioClips;

        private float waitingSeconds = 0.2f;

        [Inject]
        private IGridSystem gridSystem;
        [Inject]
        private IGameFlowManager gameFlowManager;

        private void Start()
        {
            CurrentDirection = WalkingDirection.Left;
            this.transform.localPosition = gridSystem.InitiatePlayerPosition();
            StartCoroutine(MoveCharacter());
            WaitForOneSecond = new WaitForSeconds(waitingSeconds);
            gameFlowManager.GameReset.AddListener(ResetPlayer);
        }

        private void OnDestroy()
        {
            gameFlowManager.GameReset.RemoveListener(ResetPlayer);
        }

        private void ResetPlayer()
        {
            this.transform.localPosition = gridSystem.InitiatePlayerPosition();
        }

        private IEnumerator MoveCharacter()
        {
            while (true)
            {
                this.transform.localPosition = gridSystem.GetPlayerPosition(CurrentDirection);
                audioSource.clip = audioClips[0];
                audioSource.Play();

                yield return WaitForOneSecond;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var inputValue = context.ReadValue<Vector2>();

                if (inputValue.x == 0 && inputValue.y == 1)
                {
                    CurrentDirection = WalkingDirection.Up;
                    UnityAnim.SetTrigger("MoveUp");
                }
                else if (inputValue.x == 0 && inputValue.y == -1)
                {
                    CurrentDirection = WalkingDirection.Down;
                    UnityAnim.SetTrigger("MoveDown");
                }
                else if (inputValue.x == -1 && inputValue.y == 0)
                {
                    CurrentDirection = WalkingDirection.Left;
                    UnityAnim.SetTrigger("MoveLeft");
                }
                else if (inputValue.x == 1 && inputValue.y == 0)
                {
                    CurrentDirection = WalkingDirection.Right;
                    UnityAnim.SetTrigger("MoveRight");
                }
            }
        }

        public class Factory : PlaceholderFactory<PlayerController> { }
    }
}