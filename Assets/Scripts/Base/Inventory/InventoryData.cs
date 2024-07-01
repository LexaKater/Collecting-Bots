public struct InventoryData
{
    private const Resources.Type Diamond = Resources.Type.Diamond;
    private const Resources.Type Steel = Resources.Type.Steel;
    private const Resources.Type Tree = Resources.Type.Tree;
    private const Resources.Type Meet = Resources.Type.Meet;

    private int _diamondCount;
    private int _steelCount;
    private int _treeCount;
    private int _meetCount;

    public void TakeData(Resources.Type type, int count)
    {
        switch (type)
        {
            case Diamond:
                _diamondCount += count;
                break;

            case Steel:
                _steelCount += count;
                break;

            case Tree:
                _treeCount += count;
                break;

            case Meet:
                _meetCount += count;
                break;
        }
    }

    public int GetCount(Resources.Type type)
    {
        switch (type)
        {
            case Diamond:
                return _diamondCount;

            case Steel:
                return _steelCount;

            case Tree:
                return _treeCount;

            case Meet:
                return _meetCount;
        }

        return 0;
    }
}