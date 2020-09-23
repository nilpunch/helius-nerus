using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericArtifactPool <T> : MonoBehaviour
{
    public static GenericArtifactPool<T> Instance => _instance;
    private static GenericArtifactPool<T> _instance;

    public int PoolCount => _items.Count;

    [SerializeField] private T[] _initialItems = null;
    private List<T> _items = new List<T>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _items.AddRange(_initialItems);
    }

    public void AddItemToPool(T item)
    {
        _items.Add(item);
    }

    public T TakeRandomItemFromPool()
    {
        int rnd = Random.Range(0, _items.Count);
        T item = _items[rnd];
        _items.RemoveAt(rnd);
        return item;
    }

    public void ShullfePool()
    {
        for (int i = 0; i < _items.Count; ++i)
        {
            T temp = _items[i];
            int rnd = Random.Range(0, _items.Count);
            _items[i] = _items[rnd];
            _items[rnd] = temp;
        }
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
