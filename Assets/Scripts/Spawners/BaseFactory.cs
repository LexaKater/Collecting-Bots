using UnityEngine;
using Zenject;

public class BaseFactory
{
    private const string BotBase = "House";
    
    private DiContainer _diContainer;
    private Object _basePrefab;

    private BaseFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Load() => _basePrefab = Resources.Load(BotBase);

    public GameObject Spawn(Vector3 spawnPoint)
    {
        float rotationX = -90;
        Quaternion rotation = Quaternion.Euler(rotationX, 0, 0);

        return _diContainer.InstantiatePrefab(_basePrefab, spawnPoint, rotation, null);
    }
}