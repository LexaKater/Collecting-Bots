using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private BaseInventory _inventory;

    private void OnEnable() => _inventory.ResourceCountChanged += ShowResourceCount;

    private void OnDisable() => _inventory.ResourceCountChanged -= ShowResourceCount;

    private void ShowResourceCount(int count) => _count.text = count.ToString();
}