using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SubscriberController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public class Factory : PlaceholderFactory<SubscriberController> { }
    }
}
