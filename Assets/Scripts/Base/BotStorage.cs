using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BotStorage : MonoBehaviour
{
    [SerializeField] private int _startCountBots;
    [SerializeField] private int _maxCountBots = 5;

    private List<Bot> _bots;
    private BotSpawner _botSpawner;

    [Inject]
    private void Construct(BotSpawner botSpawner) => _botSpawner = botSpawner;

    private void Awake()
    {
        _bots = new List<Bot>();

        CreateBots(_startCountBots);
    }

    public bool TryGetFreeBot(out Bot freeBot)
    {
        freeBot = null;

        foreach (Bot bot in _bots)
        {
            if (bot.IsFree)
            {
                freeBot = bot;
                return true;
            }
        }

        return false;
    }

    public bool CanCreate() => _bots.Count != _maxCountBots;

    public void AddBot(Bot bot) => _bots.Add(bot);

    public void RemoveBot(Bot bot) => _bots.Remove(bot);

    private void CreateBots(int botsCount)
    {
        for (int i = 0; i < botsCount; i++)
            _bots.Add(_botSpawner.Spawn(transform.position));
    }
}