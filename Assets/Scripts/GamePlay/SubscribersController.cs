using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SnakeGame
{
    public class SubscribersController : MonoBehaviour
    {
        [Inject]
        private Subscriber.Factory subscriber;
        [Inject]
        private IGridManager grid;
        [Inject]
        private IPlayerDataLoader playerData;

        [SerializeField]
        private Transform parent;
        [SerializeField]
        private Text subscribersDisplay;

        private GameObject subscriberToCollect;
        private Int2 subscriberGridPos;
        private Queue<GameObject> subscriberQueue = new Queue<GameObject>();
        private Queue<GameObject> subscriberPool = new Queue<GameObject>();
        private Queue<Int2> subscriberPositionQueue = new Queue<Int2>();

        void Start()
        {
            SetSubscriberDisplay(playerData.PlayerData.Subscribers);
            GenerateNewSubscriber();

            GameFlowEvents.GameReset.AddListener(ResetSubscriber);
            GameFlowEvents.SubscriberCollected.AddListener(SubscriberCollected);
            GameFlowEvents.SubscriberNotCollected.AddListener(SubscriberMoves);
        }

        private void OnDestroy()
        {
            GameFlowEvents.GameReset.RemoveListener(ResetSubscriber);
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

        private void SubscriberCollected()
        {
            SubscriberMoves();

            subscriberToCollect.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            subscriberQueue.Enqueue(subscriberToCollect.gameObject);
            subscriberPositionQueue.Enqueue(subscriberGridPos);

            playerData.PlayerData.Subscribers++;

            SetSubscriberDisplay(playerData.PlayerData.Subscribers);

            GenerateNewSubscriber();
        }

        private void SubscriberMoves()
        {
            if (playerData.PlayerData.Subscribers > 0)
            {
                foreach (var collectedSubscriber in subscriberPositionQueue)
                {
                    if (grid.CheckIfPlayerOnCoordinate(collectedSubscriber))
                    {
                        GameFlowEvents.InvokeGameOver();
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
            SetSubscriberDisplay(playerData.PlayerData.Subscribers);

            if (subscriberQueue.Count > 0)
            {
                foreach (var item in subscriberQueue)
                {
                    subscriberPool.Enqueue(item);
                    item.SetActive(false);
                }

                subscriberQueue.Clear();
                subscriberPositionQueue.Clear();
                subscriberToCollect.transform.parent = parent;
                subscriberToCollect.transform.localPosition = grid.InitiateRandomSubscriberPosition(out subscriberGridPos);
            }
        }

        private void SetSubscriberDisplay(int number)
        {
            subscribersDisplay.text = $"{number} Subscribers";
        }
    }
}