using System.Collections.Generic;
using UnityEngine;

public abstract class GenericContainer <TValue, TKey, TMod> where TValue : IconDescModifierSO<TKey>
{
    public static GenericContainer<TValue, TKey, TMod> Instance
    {
        get;
        private set;
    }

    [SerializeField] protected List<TValue> _allModifiers = null;
    [SerializeField] protected List<int> _initialUnlockedValues = null;

    // need sorted container?
    protected List<int> _lockedValues = new List<int>(); // unsorted
    protected List<int> _unlockedAvailableValues = new List<int>(); // unsorted

    public void Init()
    {
        Instance = this;

        for (int i = 0; i < _allModifiers.Count; ++i)
            _lockedValues.Add(i);

        foreach(int i in _initialUnlockedValues)
        {
            _unlockedAvailableValues.Add(i);
            _lockedValues.Remove(i);
        }

        PreCookTypes();
    }

    protected abstract void PreCookTypes();
    public abstract TMod GetArtifact(TKey key);

    public abstract TValue GetValueByKey(TKey key);

    public void UnlockNewModifier (TValue newMod)
    {
        int index = _allModifiers.IndexOf(newMod);
        _lockedValues.Remove(index);
        _unlockedAvailableValues.Add(index);
        _initialUnlockedValues.Add(index); // For saving later
    }

    // Locked functions
    public TValue GetRandomLockedModifier()
    {
        int rand = UnityEngine.Random.Range(0, _lockedValues.Count);
        // ICloneable value part?
        TValue value = _allModifiers[ _lockedValues[rand] ];
        _lockedValues.Remove(rand);
        return value;
    }
    public void ReturnLockedMod(TValue value)
    {
        _lockedValues.Add(_allModifiers.IndexOf(value));
    }

    // Unlocked functions
    public TValue GetRandomUnlockedModifier()
    {
        int rand = UnityEngine.Random.Range(0, _unlockedAvailableValues.Count);
        // ICloneable value part?
        TValue value = _allModifiers[ _unlockedAvailableValues[rand] ];
        _unlockedAvailableValues.Remove(rand);
        return value;
    }
    public void ReturnUnlockedMod(TValue value)
    {
        _unlockedAvailableValues.Add(_allModifiers.IndexOf(value));
    }
    public void RemoveArtifactFromPoolIfExists(TKey key)
    {
        TValue value = GetValueByKey(key);
        int num = _allModifiers.IndexOf(value);
        if (_unlockedAvailableValues.Contains(num))
        {
            _unlockedAvailableValues.Remove(num);
        }
    }


    public TValue GetTotallyRandom()
    {
        int rand = UnityEngine.Random.Range(0, _allModifiers.Count);
        // ICloneable value part?
        TValue value = _allModifiers[rand];
        return value;
    }
    // Save and load list of unlocked modifiers
}