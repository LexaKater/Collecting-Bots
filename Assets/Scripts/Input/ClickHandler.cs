using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private FlagSpawner _flagSpawner;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Base botBase))
            {
                _flagSpawner.TrySelectBase(botBase);
            }
            else if (hit.transform.TryGetComponent<Ground>(out _))
            {
                Vector3 spawnPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                _flagSpawner.TryPlaceFlag(spawnPoint);
            }
        }
    }
}