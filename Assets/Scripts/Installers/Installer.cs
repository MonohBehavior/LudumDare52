using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class Installer : MonoInstaller
    {
        [SerializeField]
        private PlayerDataLoader playerDataLoader;
        [SerializeField]
        private SettingsLoader settingsLoader;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerDataLoader>().To<PlayerDataLoader>().FromInstance(playerDataLoader).AsSingle();
            Container.Bind<ISettings>().To<SettingsLoader>().FromInstance(settingsLoader).AsSingle();
        }
    }
}
