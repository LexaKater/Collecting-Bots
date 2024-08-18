using UnityEngine;

[RequireComponent(typeof(BaseInventory))]
public class Base : MonoBehaviour
{
    private const int CountForCreateBot = 3;
    private const int CountForCreateBase = 5;

    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private BotSorter _botSorter;

    private BaseInventory _inventory;
    private bool _isFlagInstalled;
    private Bot _botCreator;

    public Flag Flag { get; private set; }

    private void Awake() => _inventory = GetComponent<BaseInventory>();

    private void OnEnable() => _inventory.ResourceCountChanged += OnResourceCountChanged;

    private void OnDisable() => _inventory.ResourceCountChanged -= OnResourceCountChanged;

    public void SetFlag(Flag flag)
    {
        Flag = flag;
        _isFlagInstalled = true;
    }

    public void TryRemoveFlag()
    {
        if (Flag == null)
            return;

        Destroy(Flag.gameObject);

        Flag = null;
        _isFlagInstalled = false;
    }

    private void AddBot(Bot bot) => _botSorter.AddBot(bot);

    private void OnResourceCountChanged(int countResources)
    {
        if (_isFlagInstalled)
            TryCreateBase(countResources, Flag.transform.position);
        else
            TryCreateBot(countResources);
    }


    private void TryCreateBot(int countResources)
    {
        if (countResources < CountForCreateBot)
            return;

        if (_botSorter.CanCreate() == false)
            return;

        _inventory.SpendResources(CountForCreateBot);
        _botSorter.AddBot(_botSpawner.Spawn());
    }

    private void TryCreateBase(int countResources, Vector3 flagPosition)
    {
        if (countResources < CountForCreateBase)
            return;

        if (_botSorter.TryGetFreeBot(out _botCreator) == false)
            return;

        if (_botCreator.TryGetComponent(out TargetTraсker _traсker))
            _traсker.TargetReached += OnTargetReached;

        _botCreator.GoToBuild(flagPosition);
        _botSorter.RemoveBot(_botCreator);
    }

    private void OnTargetReached()
    {
        Base botBase = _baseSpawner.Spawn(Flag.transform.position);
        botBase.AddBot(_botCreator);

        _inventory.SpendResources(CountForCreateBase);

        TryRemoveFlag();

        if (_botCreator.TryGetComponent(out TargetTraсker _traсker))
            _traсker.TargetReached -= OnTargetReached;
    }
}