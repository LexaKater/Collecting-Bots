using UnityEngine;

[RequireComponent(typeof(BaseInventory))]
public class BaseState : MonoBehaviour
{
    private const int CountForCreateBot = 3;
    private const int CountForCreateBase = 5;

    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private BotSorter _sorter;

    private BaseInventory _inventory;
    private Flag _flag;
    private bool _isFlagInstalled;

    private void Awake() => _inventory = GetComponent<BaseInventory>();

    private void OnEnable() => _inventory.ResourceCountChanged += OnCreate;

    private void OnDisable() => _inventory.ResourceCountChanged -= OnCreate;

    public void SetFlag(Flag flag)
    {
        _flag = flag;
        _isFlagInstalled = true;
    }

    public Flag GetFlag() => _flag;

    private void RemoveFlag()
    {
        if (_flag == null)
            return;

        Destroy(_flag.gameObject);

        _flag = null;
        _isFlagInstalled = false;
    }

    public void SetBot(Bot bot) => _sorter.AddBot(bot);

    private void OnCreate(int countResources)
    {
        if (_isFlagInstalled)
            CreateBase(countResources);
        else
            CreateBot(countResources);
    }

    private void CreateBot(int countResources)
    {
        if (countResources < CountForCreateBot)
            return;

        if (_sorter.CanCreate() == false)
            return;

        _inventory.SpendResources(CountForCreateBot);
        _sorter.AddBot(_botSpawner.Spawn());
    }

    private void CreateBase(int countResources)
    {
        if (countResources < CountForCreateBase)
            return;

        Bot bot = _sorter.GetFreeBot();

        if (bot == null)
            return;

        bot.GoToBuild(_flag.transform.position);
        _inventory.SpendResources(CountForCreateBase);
        _baseSpawner.StartBuildBase(bot, _flag.transform.position);
        _sorter.RemoveBot(bot);

        RemoveFlag();
    }
}