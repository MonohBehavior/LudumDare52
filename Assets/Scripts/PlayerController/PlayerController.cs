using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace SnakeGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator unityAnim;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private float waitingSeconds = 0.2f;

        [Inject]
        private IGridManager gridSystem;

        private WalkingDirection currentDirection = WalkingDirection.Left;

        private void Start()
        {
            this.transform.localPosition = gridSystem.InitiatePlayerCoordination();
            StartCoroutine(MoveCharacter());

            GameFlowEvents.GameReset.AddListener(ResetPlayer);
        }

        private void OnDestroy()
        {
            GameFlowEvents.GameReset.RemoveListener(ResetPlayer);
        }

        private void ResetPlayer()
        {
            this.transform.localPosition = gridSystem.InitiatePlayerCoordination();
        }

        private IEnumerator MoveCharacter()
        {
            var wait = new WaitForSeconds(waitingSeconds);

            while (true)
            {
                this.transform.localPosition = gridSystem.GetPlayerPosition(currentDirection);
                PlayCharacterMoveSound();

                yield return wait;
            }
        }

        private void PlayCharacterMoveSound()
        {
            audioSource.Play();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                var inputValue = context.ReadValue<Vector2>();

                if (inputValue.x == 0 && inputValue.y == 1)
                {
                    currentDirection = WalkingDirection.Up;
                    unityAnim.SetTrigger("MoveUp");
                }
                else if (inputValue.x == 0 && inputValue.y == -1)
                {
                    currentDirection = WalkingDirection.Down;
                    unityAnim.SetTrigger("MoveDown");
                }
                else if (inputValue.x == -1 && inputValue.y == 0)
                {
                    currentDirection = WalkingDirection.Left;
                    unityAnim.SetTrigger("MoveLeft");
                }
                else if (inputValue.x == 1 && inputValue.y == 0)
                {
                    currentDirection = WalkingDirection.Right;
                    unityAnim.SetTrigger("MoveRight");
                }
            }
        }
    }
}