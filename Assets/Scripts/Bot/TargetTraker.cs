using System;
using System.Collections;
using UnityEngine;

public class TargetTraсker : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget;

    private Coroutine _coroutine;

    public event Action TargetReached;

    public void StartTrackingPosition(Vector3 target)
    {
        if (_coroutine != null)
            StopCoroutine(TrackPosition(target));

        StartCoroutine(TrackPosition(target));
    }

    private IEnumerator TrackPosition(Vector3 target)
    {
        while (enabled)
        {
            if (transform.position.IsEnoughClose(target, _distanceToTarget))
                TargetReached?.Invoke();

            yield return null;
        }
    }
}