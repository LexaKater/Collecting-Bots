using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;

    private List<Vector3> _freeSpawnPoints;

    private void Awake() => _freeSpawnPoints = new List<Vector3>();

    private void Start() => StartCoroutine(TrySpawn());

    private IEnumerator TrySpawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            _freeSpawnPoints = GetFreeSpawnPoints(_spawnPoints);

            if (_freeSpawnPoints.Count != 0)
            {
                Resource currentResource = _pool.GetResource();
                currentResource.transform.SetParent(null);
                currentResource.gameObject.SetActive(true);
                currentResource.transform.position = GetRandomPosition(_freeSpawnPoints);

                currentResource.Released += OnReleaseResource;
            }

            yield return wait;
        }
    }

    private Vector3 GetRandomPosition(List<Vector3> spawnPoints) => spawnPoints[Random.Range(0, spawnPoints.Count)];

    private List<Vector3> GetFreeSpawnPoints(Transform[] spawnPoints)
    {
        _freeSpawnPoints.Clear();

        foreach (var spawnPoint in spawnPoints)
        {
            Collider[] hits = Physics.OverlapSphere(spawnPoint.transform.position, _radius, _layer);

            if (hits.Length == 0)
                _freeSpawnPoints.Add(spawnPoint.transform.position);
        }

        return _freeSpawnPoints;
    }

    private void OnReleaseResource(Resource resource)
    {
        resource.Released -= OnReleaseResource;
        _pool.PutResource(resource);
    }
}