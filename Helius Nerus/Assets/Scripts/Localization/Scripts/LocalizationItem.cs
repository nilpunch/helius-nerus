using UnityEngine;

[System.Serializable]
public class LocalizationItem
{
    [SerializeField] private string _key = "key";
    [SerializeField] private string _value = "value";
    public string Key => _key;
    public string Value => _value;
}