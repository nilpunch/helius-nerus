using UnityEngine;

public abstract class IconDescModifierSO <Type> : ScriptableObject
{
    public string Description
    {
        get => _description;
    }
    public Sprite Icon
    {
        get => _icon;
    }
    public Type Modifier
    {
        get => _modifier;
    }
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected Type _modifier;
}
