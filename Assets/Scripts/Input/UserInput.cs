using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public event Action MouseClicked;

    private void Update() => ClickMouse();

    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
            MouseClicked?.Invoke();
    }
}