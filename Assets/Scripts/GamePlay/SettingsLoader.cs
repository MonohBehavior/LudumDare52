using UnityEngine;

namespace SnakeGame
{
    public class SettingsLoader : MonoBehaviour, ISettings
    {
        public int Volume { get; set; }

        private void Awake()
        {
            Volume = PlayerPrefs.GetInt("Volume");
        }
    }
}
