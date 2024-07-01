using UnityEngine;

public class BotInventory : MonoBehaviour
{
    public Resources.Type ResourceType { get; private set; }
    public int ResourceCount { get; private set; }

    public void TakeResource(Resources resource)
    {
        ResourceType = resource.ResursesType;
        ResourceCount = resource.Count;
    }
}