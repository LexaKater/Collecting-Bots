using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private float _spawnRadius = 30f;

    public Bot Spawn()
    {
        Vector3 randomPosition = transform.position + new Vector3(GetRandomPoint(), 0, GetRandomPoint());

        Bot bot = Instantiate(_botPrefab, randomPosition, _botPrefab.transform.rotation);

        return bot;
    }

    private float GetRandomPoint() => Random.Range(-_spawnRadius, _spawnRadius);
}