using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay = 2f;

    private Resource _currentResource;
    private List<Vector3> _freeSpawnPoint;

    private void Awake() => _freeSpawnPoint = new List<Vector3>();

    private void Start() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            if (FindFreeSpawnPoint())
            {
                _currentResource = _pool.GetResource();
                _currentResource.transform.SetParent(null);
                _currentResource.gameObject.SetActive(true);
                _currentResource.transform.position = GetRandomPosition();

                _currentResource.Released += ReleaseResource;
            }

            yield return wait;
        }
    }

    private Vector3 GetRandomPosition() => _freeSpawnPoint[Random.Range(0, _freeSpawnPoint.Count)];

    private bool FindFreeSpawnPoint()
    {
        _freeSpawnPoint.Clear();

        foreach (var spawnPoint in _spawnPoints)
        {
            Collider[] hits = Physics.OverlapSphere(spawnPoint.transform.position, _radius, _layer);

            if (hits.Length == 0)
                _freeSpawnPoint.Add(spawnPoint.transform.position);
        }

        if (_freeSpawnPoint.Count == 0)
            return false;

        return true;
    }

    private void ReleaseResource(Resource resource)
    {
        resource.Released -= ReleaseResource;

        _pool.PutResource(resource);
    }
}