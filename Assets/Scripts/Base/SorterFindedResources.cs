using System.Collections.Generic;
using UnityEngine;

public class SorterFindedResources : MonoBehaviour
{
    private List<Resources> _findedRsources = new List<Resources>();

    public void AddResource(Resources resource)
    {
        bool isDetected = true;

        if (!resource.IsDetected)
        {
            resource.ChangeDetectedStatus(isDetected);
            _findedRsources.Add(resource);
        }
    }

    public Resources GetResource()
    {
        if (_findedRsources.Count > 0)
        {
            int randomIndex = Random.Range(0, _findedRsources.Count);

            Resources resource = _findedRsources[randomIndex];

            _findedRsources.Remove(_findedRsources[randomIndex]);

            return resource;
        }

        return null;
    }
}