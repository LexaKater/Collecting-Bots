using System.Collections.Generic;
using UnityEngine;

public class BotSorter : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _bots;
    private int _botCount = 3;

    private void Awake()
    {
        _bots = new List<Bot>();
        AddBots();
    }

    public Bot GetFreeBot()
    {
        foreach (Bot bot in _bots)
        {
            if (bot.IsFree)
                return bot;
        }

        return null;
    }

    private void AddBots()
    {
        for (int i = 0; i < _botCount; i++)
            _bots.Add(_botSpawner.Spawn());
    }
}