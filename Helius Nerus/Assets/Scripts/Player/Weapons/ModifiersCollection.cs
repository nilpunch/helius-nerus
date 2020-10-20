using System;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
	StraightMoveModifier,
	HomingModifier,
	RotationMoveModifier,
}

public class ModifiersCollection
{
    public static ModifiersCollection Instance { get; private set; } = null;

    public static IPlayerWeaponModifier GetRandomWeaponModifier()
    {
        int rand = UnityEngine.Random.Range(0, Instance._modifiers.Count);
        return Instance._modifiers[rand].Clone();
    }
    public static Type GetTypeByEnum(ModifierType type)
    {
        return Instance._modifiersTypes[(int)type];
    }
    public static IPlayerWeaponModifier GetModifierByEnum(ModifierType type)
    {
        return Instance._modifiers[(int)type].Clone();
    }
    public IPlayerWeaponModifier this[ModifierType type]
    {
        get => _modifiers[(int)type].Clone();
    }

    private List<Type> _modifiersTypes = new List<Type>();
    private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
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