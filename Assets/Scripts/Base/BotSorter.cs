using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSorter : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _bots;
    private List<Bot> _freeBots;
    private int _botCount = 3;

    private void Awake()
    {
        _bots = new List<Bot>();
        _freeBots = new List<Bot>();

        AddBot();
    }

    public Bot GetRandomBot()
    {
        FindFreeBoots();

        if (_freeBots.Count > 0)
            return _freeBots[Random.Range(0, _freeBots.Count)];

        return null;
    }

    private void FindFreeBoots()
    {
        _freeBots.Clear();

        foreach (Bot bot in _bots)
        {
            if (bot.IsFree)
                _freeBots.Add(bot);
        }
    }

    private void AddBot()
    {
        for (int i = 0; i < _botCount; i++)
            _bots.Add(_botSpawner.Spawn());
    }
}