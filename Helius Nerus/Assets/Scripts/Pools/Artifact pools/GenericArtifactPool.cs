using System.Collections.Generic;

public class GenericArtifactPool<T>
{
    private T[] _initialItems = null;
    private List<T> _items = new List<T>();

    public int PoolCount => _items.Count;

	public void Initialise(T[] items)
	{
		_initialItems = items;
		ResetPool();
	}

    public void AddItemToPool(T item)
    {
        _items.Add(item);
    }

	public List<T> GetAvailableItems()
	{
		return _items;
	}

    public void ClearPool()
    {
        _items.Clear();
    }

    public void ResetPool()
    {
        _items.Clear();
        _items.AddRange(_initialItems);
    }
}
