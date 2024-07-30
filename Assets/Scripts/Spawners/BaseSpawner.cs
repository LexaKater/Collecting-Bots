using System.Collections;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private BaseState _basePrefab;
    [SerializeField, Range(0, 10f)] private float _distanceToTarget;

    private Coroutine _coroutine;

    public void StartBuildBase(Bot bot, Vector3 buildPoint)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(BuildBase(bot, buildPoint));
    }

    private IEnumerator BuildBase(Bot bot, Vector3 buildPoint)
    {
        bool isWork = true;

        while (isWork)
        {
            if (bot.transform.position.IsEnoughClose(buildPoint, _distanceToTarget))
            {
                isWork = false;
                Spawn(buildPoint, bot);
            }

            yield return null;
        }
    }

    private void Spawn(Vector3 spawnPoint, Bot bot)
    {
        float rotationX = -90;
        Quaternion rotation = Quaternion.Euler(rotationX, 0, 0);

        BaseState botBase = Instantiate(_basePrefab, spawnPoint, rotation);
        botBase.SetBot(bot);
    }
}