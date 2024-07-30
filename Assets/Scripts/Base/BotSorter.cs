using System.Collections.Generic;
using UnityEngine;

public class BotSorter : MonoBehaviour
{
    private const int MaxCountBots = 5;

    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _bots;

    private void Awake()
    {
        _bots = new List<Bot>();

        CreateBots();
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

    public bool CanCreate()
    {
        if (_bots.Count == MaxCountBots)
            return false;

        return true;
    }

    public void AddBot(Bot bot) => _bots.Add(bot);

    public void RemoveBot(Bot bot) => _bots.Remove(bot);

    private void CreateBots()
    {
        int botsCount = 2;

        for (int i = 0; i < botsCount; i++)
            _bots.Add(_botSpawner.Spawn());
    }
}