[System.Serializable]
public class LocalizationData
{
    [UnityEngine.SerializeField] private LocalizationItem[] _items = null;
    public LocalizationItem[] Items => _items;
}