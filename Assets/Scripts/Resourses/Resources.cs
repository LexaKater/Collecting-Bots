using System;
using UnityEngine;

public abstract class Resources : MonoBehaviour
{
    [field: SerializeField] public Type ResursesType { get; private set; }
    [field: SerializeField] public int Count { get; private set; }

    public event Action<Resources> Released;

    public bool IsDetected { get; private set; } = false;

    public void ChangeDetectedStatus(bool isDetected) => IsDetected = isDetected;

    public void Release() => Released.Invoke(this);

    public enum Type
    {
        Diamond,
        Steel,
        Tree,
        Meet
    }
}