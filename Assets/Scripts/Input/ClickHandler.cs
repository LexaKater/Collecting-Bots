using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ClickHandler : MonoBehaviour
{
    private const int NumberMouseButton = 0;

    public event Action<Vector3> GroundClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(NumberMouseButton))
            HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out FlagHandler _flagHandler))
            {
                _flagHandler.TrySelectedBase();
            }
            else if (hit.transform.TryGetComponent<Ground>(out _))
            {
                Vector3 spawnPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                GroundClicked?.Invoke(spawnPoint);
            }
        }
    }
}