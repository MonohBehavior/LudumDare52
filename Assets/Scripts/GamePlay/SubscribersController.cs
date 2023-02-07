using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SnakeGame
{
    public class SubscribersController : MonoBehaviour
    {
        [Inject]
        private SubscriberController.Factory subscriber;
        [Inject]
        private IGameFlowManager gameFlow;
        [Inject]
        private IGridSystem grid;
        [Inject]
        private IPlayerDataLoader playerData;

        private GameObject subscriberToCollect;
        private Queue<GameObject> subscriberQueue = new Queue<GameObject>();
        private Queue<GameObject> subscriberPool = new Queue<GameObject>();
        private Queue<Int2> subscriberPositionQueue = new Queue<Int2>();

        [SerializeField]
        private Transform parent;
        [SerializeField]
        private Text subscribersDisplay;
        [SerializeField]
        private StageInfoSO stageInfo;

        private Int2 subscriberGridPos;

        void Start()
        {
            subscribersDisplay.text = $"{playerData.PlayerData.Subscribers} Subscribers";
            GenerateNewSubscriber();

            gameFlow.GameReset.AddListener(ResetSubscriber);
            grid.SubscriberCollected.AddListener(SubscriberFollows);
            grid.SubscriberNotCollected.AddListener(SubscriberMoves);
        }

        private void OnDestroy()
        {
            gameFlow.GameReset.RemoveListener(ResetSubscriber);
        }

        private void GenerateNewSubscriber()
        {
            if (subscriberPool.Count == 0)
            {
                subscriberToCollect = subscriber.Create().gameObject;
            }
            else
            {
                subscriberToCollect = subscriberPool.Dequeue();
                subscriberToCollect.SetActive(true);
            }

            subscriberToCollect.transform.parent = parent;
            subscriberToCollect.transform.localPosition = grid.InitiateRandomSubscriberPosition(out subscriberGridPos);
            subscriberToCollect.transform.localScale = Vector3.one * 96;
        }

        private void SubscriberFollows()
        {
            SubscriberMoves();

            subscriberToCollect.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            subscriberQueue.Enqueue(subscriberToCollect.gameObject);
            subscriberPositionQueue.Enqueue(subscriberGridPos);
            playerData.PlayerData.Subscribers++;
            subscribersDisplay.text = $"{playerData.PlayerData.Subscribers} Subscribers";

            if (CheckGoalAchieved())
            {
                gameFlow.InvokeGameClear();
                return;
            }

            GenerateNewSubscriber();
        }

        private void SubscriberMoves()
        {
            if (playerData.PlayerData.Subscribers > 0)
            {
                foreach (var item in subscriberPositionQueue)
                {
                    if (grid.CheckPlayerColliding(item))
                    {
                        gameFlow.InvokeGameOver();
                        return;
                    }
                }

                var tempSubscriber = subscriberQueue.Dequeue();
                subscriberPositionQueue.Dequeue();
                tempSubscriber.transform.localPosition = grid.GetPlayerPreviousPosition(out subscriberGridPos);

                subscriberQueue.Enqueue(tempSubscriber);
                subscriberPositionQueue.Enqueue(subscriberGridPos);
            }
        }

        private void ResetSubscriber()
        {
            playerData.PlayerData.Subscribers = 0;
            subscribersDisplay.text = $"{playerData.PlayerData.Subscribers} Subscribers";

            if (subscriberQueue.Count > 0)
            {
                foreach (var item in subscriberQueue)
                {
                    subscriberPool.Enqueue(item);
                    item.SetActive(false);
                }

                subscriberQueue.Clear();
                //subscriberToCollect = subscriberQueue.Dequeue();
                //subscriberToCollect.SetActive(true);
                subscriberPositionQueue.Clear();
                subscriberToCollect.transform.parent = parent;
                subscriberToCollect.transform.localPosition = grid.InitiateRandomSubscriberPosition(out subscriberGridPos);
            }
        }

        private bool CheckGoalAchieved()
        {
            return playerData.PlayerData.Subscribers >= stageInfo.SubscriberGoal;
        }
    }
}