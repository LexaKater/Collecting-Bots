using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    public Base Spawn(Vector3 spawnPoint)
    {
        float rotationX = -90;
        Quaternion rotation = Quaternion.Euler(rotationX, 0, 0);

        return Instantiate(_basePrefab, spawnPoint, rotation);
    }
}