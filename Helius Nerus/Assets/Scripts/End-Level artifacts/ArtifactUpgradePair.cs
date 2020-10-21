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
        int rand = UnityEngine.Random.Range(0, 2);
        modifier = ModifiersCollection.GetRandomWeaponModifier();
        return new ArtifactUpgradePair(parametrs, modifier);
    }
}
