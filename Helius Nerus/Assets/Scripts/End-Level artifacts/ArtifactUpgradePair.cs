public class ArtifactUpgradePair
{
    public IPlayerArtifact Artifact
    {
        get => _artifact;
    }
    public IPlayerWeaponModifier WeaponModifier
    {
        get => _modifier;
    }
    public PlayerWeaponsParametrs WeaponsParametrs
    {
        get => _params;
    }
    public bool IsWeaponMod
    {
        get => _modifier != null;
    }
    public bool IsShipMod
    {
        get => _artifact != null;
    }


    private IPlayerArtifact _artifact = null;
    private IPlayerWeaponModifier _modifier = null;
    private PlayerWeaponsParametrs _params;

    public ArtifactUpgradePair(PlayerWeaponsParametrs playerWeaponsParametrs, IPlayerArtifact artifact = null, IPlayerWeaponModifier modifier = null)
    {
        _params = playerWeaponsParametrs.Clone();
        if (artifact != null)
            _artifact = artifact.Clone();
        if (modifier != null)
            _modifier = modifier.Clone();
    }

    public static ArtifactUpgradePair CreateRandomPair()
    {
        PlayerWeaponsParametrs parametrs = EndLevelUpgradeCollection.Instance.GetRandomUpgrade();
        IPlayerArtifact artifact = null;
        IPlayerWeaponModifier modifier = null;
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
        {
            artifact = ArtifactsCollection.GetRandomPlayerArtifact();
        }
        else
        {
            modifier = ModifiersCollection.GetRandomWeaponModifier();
        }
        return new ArtifactUpgradePair(parametrs, artifact, modifier);
    }
}
