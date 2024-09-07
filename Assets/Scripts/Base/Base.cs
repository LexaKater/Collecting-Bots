using UnityEngine;
using Zenject;

[RequireComponent(typeof(BaseInventory))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _countForCreateBase;
    [SerializeField] private int _countForCreateBot;
    [SerializeField] private BotStorage _botStorage;

    private BaseInventory _inventory;
    private FlagSpawner _flagSpawner;
    private BaseFactory _baseFactory;
    private BotSpawner _botSpawner;
    private bool _isFlagInstalled;

    [Inject]
    private void Construct(FlagSpawner flagSpawner, BotSpawner botSpawner, BaseFactory baseFactory)
    {
        _flagSpawner = flagSpawner;
        _baseFactory = baseFactory;
        _botSpawner = botSpawner;
    }

    public Flag Flag { get; private set; }

    private void Awake() => _inventory = GetComponent<BaseInventory>();

    private void Start()
    {
        _baseFactory.Load();
        CreateFlag();
    }

    private void OnEnable() => _inventory.ResourceCountChanged += OnResourceCountChanged;

    private void OnDisable() => _inventory.ResourceCountChanged -= OnResourceCountChanged;

    public void PutFlag()
    {
        Flag.gameObject.SetActive(true);
        _isFlagInstalled = true;
    }

    public void RemoveFlag()
    {
        Flag.gameObject.SetActive(false);
        _isFlagInstalled = false;
    }

    private void CreateFlag()
    {
        Flag = _flagSpawner.SpawnFlag(transform.position);

        Flag.transform.SetParent(transform);
        Flag.gameObject.SetActive(false);
    }

    private void AddBot(Bot bot) => _botStorage.AddBot(bot);

    private void OnResourceCountChanged(int countResources)
    {
        if (_isFlagInstalled)
            TryCreateBase(countResources, Flag.transform.position);
        else
            TryCreateBot(countResources);
    }

    private void TryCreateBot(int countResources)
    {
        if (countResources < _countForCreateBot)
            return;

        if (_botStorage.CanCreate() == false)
            return;

        _inventory.SpendResources(_countForCreateBot);
        _botStorage.AddBot(_botSpawner.Spawn(transform.position));
    }

    private void TryCreateBase(int countResources, Vector3 flagPosition)
    {
        if (countResources < _countForCreateBase)
            return;

        if (_botStorage.TryGetFreeBot(out Bot botCreator) == false)
            return;

        botCreator.TargetReached += OnTargetReached;

        botCreator.GoToBuild(flagPosition);
        _inventory.SpendResources(_countForCreateBase);
        _botStorage.RemoveBot(botCreator);
    }

    private void OnTargetReached(Bot botCreator)
    {
        botCreator.TargetReached -= OnTargetReached;

        var botBase = _baseFactory.Spawn(Flag.transform.position);

        if (botBase.TryGetComponent(out Base Basebot))
            Basebot.AddBot(botCreator);

        RemoveFlag();
    }
}