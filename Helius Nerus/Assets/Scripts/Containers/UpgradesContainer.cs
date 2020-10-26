using System;

[Serializable]
public class UpgradesContainer : GenericContainer<StatUpgradeIconDesc, PlayerWeaponsParametrs, PlayerWeaponsParametrs>
{
    public override StatUpgradeIconDesc GetValueByKey(PlayerWeaponsParametrs key)
    {
        for (int i = 0; i < _allModifiers.Count; ++i)
        {
            if (_allModifiers[i].Modifier == key)
                return _allModifiers[i];
        }
        return null;
    }

    protected override PlayerWeaponsParametrs GetArtifact(PlayerWeaponsParametrs key)
    {
        return key;
    }

    protected override void PreCookTypes()
    {
        return;
    }
}

