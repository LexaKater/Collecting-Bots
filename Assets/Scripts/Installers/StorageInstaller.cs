using UnityEngine;
using Zenject;

public class StorageInstaller : MonoInstaller
{
    [SerializeField] private ResourcesStorage _storage;
    
    public override void InstallBindings()
    {
        Container.Bind<ResourcesStorage>().FromComponentInNewPrefab(_storage).AsSingle();
    }
}