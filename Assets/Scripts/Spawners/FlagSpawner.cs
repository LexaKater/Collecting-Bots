using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    public Flag SpawnFlag(Vector3 spawnPoint) => Instantiate(_flag, spawnPoint, Quaternion.identity);
}