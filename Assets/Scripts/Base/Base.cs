using UnityEngine;
using Zenject;

[RequireComponent(typeof(BaseInventory), typeof(FlagHandler))]
public class Base : MonoBehaviour
{
    [SerializeField] private int _countForCreateBase;
    [SerializeField] private int _countForCreateBot;
    [SerializeField] private BotStorage _botStorage;

    private FlagHandler _flagHandler;
    private BaseInventory _inventory;
    private BaseFactory _baseFactory;
    private BotSpawner _botSpawner;

    [Inject]
    private void Construct(BotSpawner botSpawner, BaseFactory baseFactory)
    {
        _baseFactory = baseFactory;
        _botSpawner = botSpawner;
    }

    private void Awake()
    {
        _flagHandler = GetComponent<FlagHandler>();
        _inventory = GetComponent<BaseInventory>();
    }

    private void Start() => _baseFactory.Load();

    private void OnEnable() => _inventory.ResourceCountChanged += OnResourceCountChanged;

    private void OnDisable() => _inventory.ResourceCountChanged -= OnResourceCountChanged;

    private void AddBot(Bot bot) => _botStorage.AddBot(bot);

    private void OnResourceCountChanged(int countResources)
    {
        if (_flagHandler.IsFlagInstalled)
            TryCreateBase(countResources, _flagHandler.Flag.transform.position);
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

        var botBase = _baseFactory.Spawn(_flagHandler.Flag.transform.position);

        if (botBase.TryGetComponent(out Base Basebot))
            Basebot.AddBot(botCreator);

        _flagHandler.RemoveFlag();
    }
}