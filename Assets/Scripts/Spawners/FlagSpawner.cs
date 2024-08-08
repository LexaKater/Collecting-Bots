using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private UserInput _input;
    [SerializeField] private Camera _camera;
    [SerializeField] private Flag _flag;

    private bool _isBaseSelected = false;
    private Flag _currentFlag;
    private Base _selectedBase;

    private void OnEnable() => _input.MouseClicked += OnHandleMouseClick;

    private void OnDisable() => _input.MouseClicked -= OnHandleMouseClick;

    private void OnHandleMouseClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (_isBaseSelected)
                PlaceFlag(hit);
            else
                SelectBase(hit);
        }
    }

    private void PlaceFlag(RaycastHit hit)
    {
        Vector3 spawnPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);

        if (hit.transform.TryGetComponent<Ground>(out _))
        {
            if (_currentFlag == null)
            {
                _currentFlag = Instantiate(_flag, spawnPoint, Quaternion.identity);
                _currentFlag.transform.SetParent(_selectedBase.transform);
                _selectedBase.SetFlag(_currentFlag);
            }
            else
            {
                _currentFlag.transform.position = spawnPoint;
            }

            _selectedBase = null;
            _isBaseSelected = false;
        }
    }

    private void SelectBase(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out Base botBase))
        {
            _selectedBase = botBase;
            _currentFlag = botBase.Flag;
            _isBaseSelected = true;
        }
    }
}