using System;
using System.Collections.Generic;
using UnityEngine;

public class ModifiersCollection : MonoBehaviour
{
    public static ModifiersCollection Instance { get; private set; } = null;

    private List<Type> _modifiersTypes = new List<Type>();
    private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();

    public static Type GetTypeByEnum(ModifierType type)
    {
        return Instance._modifiersTypes[(int)type];
    }
    public static IPlayerWeaponModifier GetCommandByEnum(ModifierType type)
    {
        return Instance._modifiers[(int)type].Clone();
    }
    public IPlayerWeaponModifier this[ModifierType type]
    {
        get => _modifiers[(int)type].Clone();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        PreCookTypes();
    }

    private void PreCookTypes()
    {
        _modifiersTypes.Clear();
        _modifiers.Clear();
        foreach (ModifierType type in (ModifierType[])Enum.GetValues(typeof(ModifierType)))
        {
            Type ctype = Type.GetType(type.ToString());
#if UNITY_EDITOR
            if (ctype == null)
            {
                Debug.LogError("ModifiersCollection handle wrong modifier name: " + type.ToString());
                Debug.Break();
            }
#endif
            _modifiersTypes.Add(ctype);
            _modifiers.Add((IPlayerWeaponModifier)Activator.CreateInstance(ctype));
        }
    }
}

public enum ModifierType
{
    BlankModifier,
}