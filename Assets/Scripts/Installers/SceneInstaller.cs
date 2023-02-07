using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private GridSystem GridSystem;
        [SerializeField]
        private GameFlowManager GameFlowManager;
        [SerializeField]
        private SubscriberController Subscriber;

        public override void InstallBindings()
        {
            Container.Bind<IGridSystem>().To<GridSystem>().FromInstance(GridSystem).AsSingle();
            Container.Bind<IGameFlowManager>().To<GameFlowManager>().FromInstance(GameFlowManager).AsSingle();
            Container.BindFactory<SubscriberController, SubscriberController.Factory>().FromComponentInNewPrefab(Subscriber);
        }
    }
}