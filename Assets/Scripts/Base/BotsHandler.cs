using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotsHandler : MonoBehaviour
{
    [SerializeField] private BotMover[] _bots;
    [SerializeField] private Scaner _scaner;
    [SerializeField] private SorterFindedResources _sorter;

    private List<BotMover> _freeBots;

    private void Awake() => _freeBots = new List<BotMover>();

    private void Update() => StartCollection();

    private void OnEnable() => _scaner.ResourceFinded += _sorter.AddResource;

    private void OnDisable() => _scaner.ResourceFinded -= _sorter.AddResource;

    private void StartCollection()
    {
        FindFreeBoots();
        BotMover freeBot = GetRandomBot();

        if (freeBot == null)
            return;

        Resources resource = _sorter.GetResource();

        if (resource == null)
            return;

        freeBot.StartMove(resource);

        _freeBots.Clear();
    }

    private void FindFreeBoots()
    {
        foreach (BotMover bot in _bots)
        {
            if (bot.IsFree)
                _freeBots.Add(bot);
        }
    }

    private BotMover GetRandomBot()
    {
        if (_freeBots.Count > 0)
            return _freeBots[Random.Range(0, _freeBots.Count)];

        return null;
    }
}