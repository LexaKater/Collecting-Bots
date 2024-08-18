using System;
using System.Collections;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private CollectionHandler _collectionHandler;
    [SerializeField] private Transform _scanPoint;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;

    public event Action<Resource> ResourceFinded;

    private void Start() => StartCoroutine(Scan());

    private IEnumerator Scan()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Collider[] hits = Physics.OverlapSphere(_scanPoint.position, _radius, _layer);

            if (hits != null)
                SearchResources(hits);

            yield return wait;
        }
    }

    private void SearchResources(Collider[] hits)
    {
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
            {
                ResourceFinded?.Invoke(resource);
                _collectionHandler.TrySendToCollect();
            }
        }
    }
}