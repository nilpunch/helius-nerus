using System;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierType
{
	StraightMoveModifier,
	HomingModifier,
	RotationMoveModifier,
	RicochetModifier,
	ExplosionModifier,
	SplitInTwoModifier,
	SprayModifier,
}

public class ModifiersCollection
{
    public static ModifiersCollection Instance { get; private set; } = null;

    public static PlayerWeaponModifier GetRandomWeaponModifier()
    {
        int rand = UnityEngine.Random.Range(0, Instance._modifiers.Count);
        return Instance._modifiers[rand].Clone();
    }
    public static Type GetTypeByEnum(ModifierType type)
    {
        return Instance._modifiersTypes[(int)type];
    }
    public static PlayerWeaponModifier GetModifierByEnum(ModifierType type)
    {
        return Instance._modifiers[(int)type].Clone();
    }

    public static  void UnlockNewModifier(ModifierType modifierType)
    {
        Instance._unlockedModifiers.Add(modifierType);
        Instance._availableDuringRunModifiers.Add(GetModifierByEnum(modifierType));
    }
    public static PlayerWeaponModifier GetRandomModifierFromPool()
    {
        int rand = UnityEngine.Random.Range(0, Instance._availableDuringRunModifiers.Count);
        PlayerWeaponModifier result = Instance._availableDuringRunModifiers[rand].Clone();
        Instance._availableDuringRunModifiers.RemoveAt(rand);
        return result;
    }
    public static void ReturnModifierToPool(PlayerWeaponModifier modifier)
    {
        Instance._availableDuringRunModifiers.Add(modifier);
    }

    public PlayerWeaponModifier this[ModifierType type]
    {
        get => _modifiers[(int)type].Clone();
    }

    private List<Type> _modifiersTypes = new List<Type>();
    private List<PlayerWeaponModifier> _modifiers = new List<PlayerWeaponModifier>();

    private List<ModifierType> _unlockedModifiers = new List<ModifierType>();
    private List<PlayerWeaponModifier> _availableDuringRunModifiers = new List<PlayerWeaponModifier>();

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
        LoadUnlockedModifiers();
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
            _modifiers.Add((PlayerWeaponModifier)Activator.CreateInstance(ctype));
        }
    }

    private void LoadUnlockedModifiers()
    {
        // for Энамы модификаторов : какой-то список (файлик, жсон...)
        // Анлок (модификатор)
        // Временно:
        foreach (ModifierType type in (ModifierType[])Enum.GetValues(typeof(ModifierType)))
        {
            UnlockNewModifier(type);
        }
    }
}