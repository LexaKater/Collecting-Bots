using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private bool _isBaseSelected = false;
    private Base _selectedBase;

    public Flag SpawnFlag(Vector3 spawnPoint) => Instantiate(_flag, spawnPoint, Quaternion.identity);

    public void TryPlaceFlag(Vector3 spawnPoint)
    {
        if (_isBaseSelected)
        {
            if (_selectedBase.Flag != null)
            {
                _selectedBase.PutFlag();
                _selectedBase.Flag.transform.position = spawnPoint;
            }

            _selectedBase = null;
            _isBaseSelected = false;
        }
    }

    public void TrySelectBase(Base botBase)
    {
        if (_isBaseSelected)
        {
            RemoveFlag();
        }
        else
        {
            _selectedBase = botBase;
            _isBaseSelected = true;
        }
    }

    private void RemoveFlag()
    {
        _selectedBase.RemoveFlag();
        
        _selectedBase = null;
        _isBaseSelected = false;
    }
}