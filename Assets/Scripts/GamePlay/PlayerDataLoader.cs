using UnityEngine;
using Newtonsoft.Json;

namespace SnakeGame
{
    public class PlayerDataLoader : MonoBehaviour, IPlayerDataLoader
    {
        public PlayerData PlayerData { get; set; }

        void Awake()
        {
            PlayerData = new PlayerData();

            if (PlayerPrefs.HasKey("Initiated"))
            {
                var savedData = PlayerPrefs.GetString("PlayerData");
                PlayerData = JsonConvert.DeserializeObject<PlayerData>(savedData);
            }
        }
    }
}