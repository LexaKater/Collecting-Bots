using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private float _spawnRadius;

    public Bot Spawn()
    {
        Vector3 randomPosition = transform.position +
                                 new Vector3(GetRandomNumber(-_spawnRadius, _spawnRadius), 0,
                                     GetRandomNumber(-_spawnRadius, _spawnRadius));

        return Instantiate(_botPrefab, randomPosition, _botPrefab.transform.rotation);
    }

    private float GetRandomNumber(float minNumber, float maxNumber) => Random.Range(minNumber, maxNumber);
}