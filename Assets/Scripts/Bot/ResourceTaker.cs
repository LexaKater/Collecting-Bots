using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class ResourceTaker : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget = 0.1f;
    
    private Resource _currentResource;
    
    private void Update()
    {
        if (_currentResource != null)
            TakeResource();
    }

    public void SetResource(Resource resource) => _currentResource = resource;

    private void TakeResource()
    {
        if (transform.position.IsEnoughClose(_currentResource.transform.position, _distanceToTarget))
        {
            _currentResource.transform.SetParent(transform);
            _currentResource.transform.localPosition = Vector3.forward * _distanceToTarget;
        }
    }
}