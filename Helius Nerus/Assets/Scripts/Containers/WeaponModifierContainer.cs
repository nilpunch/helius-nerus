using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponModifierContainer : GenericContainer<WeaponArtifactIconDesc, ModifierType, PlayerWeaponModifier>
{
    private List<Type> _artifactTypes = new List<Type>();
    private List<PlayerWeaponModifier> _artifacts = new List<PlayerWeaponModifier>();

    public override WeaponArtifactIconDesc GetValueByKey(ModifierType key)
    {
        for (int i = 0; i < _allModifiers.Count; ++i)
        {
            if (_allModifiers[i].Modifier == key)
                return _allModifiers[i];
        }
        return null;
    }

    public override PlayerWeaponModifier GetArtifact(ModifierType key)
    {
        return _artifacts[(int)key].Clone();
    }

    protected override void PreCookTypes()
    {
        _artifactTypes.Clear();
        _artifacts.Clear();
        foreach (ModifierType type in (ModifierType[])Enum.GetValues(typeof(ModifierType)))
        {
            Type ctype = Type.GetType(type.ToString());
#if UNITY_EDITOR
            if (ctype == null)
            {
                Debug.LogError("WeaponModifiersCollection handle wrong artifact name: " + type.ToString());
                Debug.Break();
            }
#endif
            _artifactTypes.Add(ctype);
            _artifacts.Add((PlayerWeaponModifier)Activator.CreateInstance(ctype));
        }
    }
}

