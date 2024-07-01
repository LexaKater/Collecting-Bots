using System.Collections;
using UnityEngine;

public class ResoursesSpawner : MonoBehaviour
{
    [SerializeField] private ResoursesPool _pool;
    [SerializeField] private Transform[] _spawnPoints;

    private Resources _currentResouce;
    private float _delayBetweenSpawn = 2f;

    private void Start() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_delayBetweenSpawn);

        bool isDetected = false;

        while (enabled)
        {
            _currentResouce = _pool.GetResource();

            _currentResouce.Released += _pool.OnPutResource;

            _currentResouce.ChangeDetectedStatus(isDetected);
            _currentResouce.gameObject.SetActive(true);
            _currentResouce.transform.position = GetRandomSpawnPoint();

            yield return wait;
        }
    }

    private Vector3 GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
}