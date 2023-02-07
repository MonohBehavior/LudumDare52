using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class TestingScripts : MonoBehaviour
    {
        [Inject]
        private IPlayerDataLoader playerData;

        void Start()
        {
            Debug.Log(playerData.PlayerData.LastStage);
        }
    }
}