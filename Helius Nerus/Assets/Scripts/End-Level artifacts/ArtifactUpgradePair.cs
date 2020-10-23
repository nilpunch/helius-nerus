public class ArtifactUpgradePair
{
    public IPlayerWeaponModifier WeaponModifier
    {
        get => _modifier;
    }
    public PlayerWeaponsParametrs WeaponsParametrs
    {
        get => _params;
    }

    private IPlayerWeaponModifier _modifier = null;
    private PlayerWeaponsParametrs _params;

    public ArtifactUpgradePair(PlayerWeaponsParametrs playerWeaponsParametrs, IPlayerWeaponModifier modifier)
    {
        _params = playerWeaponsParametrs.Clone();
        _modifier = modifier.Clone();
    }

    public static ArtifactUpgradePair CreateRandomPair()
    {
        PlayerWeaponsParametrs parametrs = EndLevelUpgradeCollection.Instance.GetRandomUpgrade();
        IPlayerWeaponModifier modifier = null;
        modifier = ModifiersCollection.GetRandomWeaponModifier();
        return new ArtifactUpgradePair(parametrs, modifier);
    }

    public string Description
    {
        get
        {
            return LocalizationManager.Instance.GetLocalizedValue(_modifier.Description)
                + "\n"
                + LocalizationManager.Instance.GetLocalizedValue(_params.Description);
        }
    }
}
