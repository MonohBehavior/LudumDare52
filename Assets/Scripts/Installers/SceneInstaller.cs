using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private GridManager GridSystem;
        [SerializeField]
        private Subscriber Subscriber;

        public override void InstallBindings()
        {
            Container.Bind<IGridManager>().To<GridManager>().FromInstance(GridSystem).AsSingle();
            Container.BindFactory<Subscriber, Subscriber.Factory>().FromComponentInNewPrefab(Subscriber);
        }
    }
}