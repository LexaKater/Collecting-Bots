using TMPro;
using UnityEngine;

public class ResourcesView : MonoBehaviour
{
    private const Resources.Type Diamond = Resources.Type.Diamond;
    private const Resources.Type Steel = Resources.Type.Steel;
    private const Resources.Type Tree = Resources.Type.Tree;
    private const Resources.Type Meet = Resources.Type.Meet;

    [SerializeField] private TextMeshProUGUI _diamondCount;
    [SerializeField] private TextMeshProUGUI _steelCount;
    [SerializeField] private TextMeshProUGUI _treeCount;
    [SerializeField] private TextMeshProUGUI _meetCount;

    public void ShowResourcesCount(Resources.Type type, int count)
    {
        switch (type)
        {
            case Diamond:
                _diamondCount.text = count.ToString();
                break;

            case Steel:
                _steelCount.text = count.ToString();
                break;

            case Tree:
                _treeCount.text = count.ToString();
                break;

            case Meet:
                _meetCount.text = count.ToString();
                break;
        }
    }
}