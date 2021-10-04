using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.Bind<MainGameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PoolManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
    }
}