using System;
using System.Collections;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private Transform _centrPoint;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;

    private float _delayBetweenScan = 1;

    public event Action<Resources> ResourceFinded;

    private void Start() => StartCoroutine(StartScan());

    private IEnumerator StartScan()
    {
        WaitForSeconds wait = new WaitForSeconds(_delayBetweenScan);

        while (enabled)
        {
            Collider[] hits = Physics.OverlapSphere(_centrPoint.position, _radius, _layer);

            if (hits != null)
                SearchResources(hits);

            yield return wait;
        }
    }

    private void SearchResources(Collider[] hits)
    {
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resources resource))
                ResourceFinded?.Invoke(resource);
        }
    }
}