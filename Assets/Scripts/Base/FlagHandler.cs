using UnityEngine;
using Zenject;

public class FlagHandler : MonoBehaviour
{
    private bool _isBaseSelected = false;
    private ClickHandler _clickHandler;
    private FlagSpawner _flagSpawner;

    [Inject]
    private void Construct(FlagSpawner flagSpawner, ClickHandler clickHandler)
    {
        _flagSpawner = flagSpawner;
        _clickHandler = clickHandler;
    }

    public bool IsFlagInstalled { get; private set; }

    public Flag Flag { get; private set; }

    private void Start() => CreateFlag();

    private void OnEnable() => _clickHandler.GroundClicked += OnGroundClicked;

    private void OnDisable() => _clickHandler.GroundClicked -= OnGroundClicked;

    public void RemoveFlag()
    {
        Flag.gameObject.SetActive(false);
        IsFlagInstalled = false;
        _isBaseSelected = false;
    }

    public void TrySelectedBase()
    {
        if (_isBaseSelected)
            RemoveFlag();
        else
            _isBaseSelected = true;
    }

    private void PutFlag()
    {
        Flag.gameObject.SetActive(true);
        IsFlagInstalled = true;
    }

    private void CreateFlag()
    {
        Flag = _flagSpawner.SpawnFlag(transform.position);

        Flag.transform.SetParent(transform);
        Flag.gameObject.SetActive(false);
    }

    private void OnGroundClicked(Vector3 spawnPoint)
    {
        if (_isBaseSelected)
        {
            PutFlag();
            Flag.transform.position = spawnPoint;
            _isBaseSelected = false;
        }
    }
}