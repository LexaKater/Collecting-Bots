using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerDiedSignalObserver
    {
        public void OnPlayerDied()
        {
            Debug.Log("Fired PlayerDiedSignal");
        }
    }
}