using UnityEngine;

public class ResourcesTaker : MonoBehaviour
{
    [SerializeField] private BotInventory _inventory;

    public void TakeResource(Resources resource)
    {
        float distanceTotarget = 0.5f;

        if (transform.position.IsEnoughClose(resource.transform.position, distanceTotarget))
        {
            resource.Release();
            _inventory.TakeResource(resource);
        }
    }
}