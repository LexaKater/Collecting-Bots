using UnityEngine;

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private bool _isBaseSelected = false;
    private Flag _currentFlag;
    private Base _selectedBase;

    public void TryPlaceFlag(Vector3 spawnPoint)
    {
        if (_isBaseSelected)
        {
            if (_currentFlag == null)
            {
                _currentFlag = Instantiate(_flag, spawnPoint, Quaternion.identity);
                _currentFlag.transform.SetParent(_selectedBase.transform);
                _selectedBase.SetFlag(_currentFlag);
            }
            else
                _currentFlag.transform.position = spawnPoint;

            _selectedBase = null;
            _isBaseSelected = false;
        }
    }

    public void TrySelectBase(Base botBase)
    {
        if (_isBaseSelected)
            RemoveFlag();

        _selectedBase = botBase;
        _currentFlag = botBase.Flag;
        _isBaseSelected = true;
    }

    private void RemoveFlag()
    {
        _selectedBase.TryRemoveFlag();

        _currentFlag = null;
        _selectedBase = null;
        _isBaseSelected = false;
    }
}