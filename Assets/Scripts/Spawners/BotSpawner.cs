using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRadius;
    [SerializeField] private Bot _botPrefab;

    public Bot Spawn(Vector3 startPosition)
    {
        Vector3 randomPosition = startPosition +
                                 new Vector3(Random.Range(-_spawnRadius, _spawnRadius), 0,
                                     Random.Range(-_spawnRadius, _spawnRadius));

        return Instantiate(_botPrefab, randomPosition, _botPrefab.transform.rotation);
    }
}