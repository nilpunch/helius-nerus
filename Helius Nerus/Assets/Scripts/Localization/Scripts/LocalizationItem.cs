using UnityEngine;

[System.Serializable]
public class LocalizationItem
{
    public string Key => _key;
    public string Value => _value;
    [SerializeField] private string _key = "key";
    [SerializeField] private string _value = "value";
}