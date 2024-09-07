using Zenject;
using UnityEngine;

public class SpawnersInstaller : MonoInstaller
{
    [SerializeField] private FlagSpawner _flagSpawner;
    [SerializeField] private BotSpawner _botSpawner;

    public override void InstallBindings()
    {
        Container.Bind<FlagSpawner>().FromComponentInNewPrefab(_flagSpawner).AsSingle();
        Container.Bind<BotSpawner>().FromComponentInNewPrefab(_botSpawner).AsSingle();
        Container.Bind<BaseFactory>().FromNew().AsSingle();
    }
}