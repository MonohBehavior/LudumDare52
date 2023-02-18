using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class Subscriber : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Subscriber> { }
    }
}
