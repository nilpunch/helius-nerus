public class ArtifactUpgradePair
{
    public IPlayerWeaponModifier WeaponModifier
    {
        get => WeaponModifierContainer.Instance.GetArtifact(_modifierID.Modifier);
    }
    public PlayerWeaponsParametrs WeaponsParametrs
    {
        get => _paramsID.Modifier;
    }
    public UnityEngine.Sprite ModifierIcon
    {
        get => _modifierID.Icon;
    }
    public UnityEngine.Sprite StatUpgradeIcon
    {
        get => _paramsID.Icon;
    }
    public WeaponArtifactIconDesc ModifierID
    {
        get => _modifierID;
    }


    private WeaponArtifactIconDesc _modifierID;
    private StatUpgradeIconDesc _paramsID;

    public ArtifactUpgradePair(StatUpgradeIconDesc playerWeaponsParametrs, WeaponArtifactIconDesc modifier)
    {
        _paramsID = playerWeaponsParametrs;
        _modifierID = modifier;
    }

    public static ArtifactUpgradePair CreateRandomPair()
    {
        WeaponArtifactIconDesc modifierID = WeaponModifierContainer.Instance.GetRandomUnlockedModifier();
        StatUpgradeIconDesc paramsID = UpgradesContainer.Instance.GetTotallyRandom();

        return new ArtifactUpgradePair(paramsID, modifierID);
    }

    public string Description
    {
        get
        {
            return LocalizationManager.Instance.GetLocalizedValue(_modifierID.Description)
                + "\n"
                + LocalizationManager.Instance.GetLocalizedValue(_paramsID.Description);
        }
    }
}
