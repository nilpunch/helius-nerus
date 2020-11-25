public class DoubleModifierArtifact : PlayerArtifact
{
    public override string MyEnumName => "DoubleModifierArtifact";

    public override PlayerArtifact Clone()
    {
        return (PlayerArtifact)this.MemberwiseClone();
    }

    public override void OnPick()
    {
        PlayerWeapon.ModifierApply += PlayerWeapon_ModifierApply;
    }

    private void PlayerWeapon_ModifierApply(PlayerWeapon obj, ArtifactUpgradePair pair)
    {
        foreach (PlayerWeapon weapon in Player.PlayerWeapons)
        {
            if (object.ReferenceEquals(weapon, obj) == false)
            {
                weapon.ApplyPair(pair, true);
            }
        }
    }

    public override void OnDrop()
    {
        PlayerWeapon.ModifierApply -= PlayerWeapon_ModifierApply;
    }
}
