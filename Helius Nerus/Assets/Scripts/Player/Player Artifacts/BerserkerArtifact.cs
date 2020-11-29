public class BerserkerArtifact : PlayerArtifact
{
    public override string MyEnumName => "BerserkerArtifact";

    public override ArtifactType MyEnum => ArtifactType.BerserkerArtifact;

    public override PlayerArtifact Clone()
    {
        return (BerserkerArtifact)this.MemberwiseClone();
    }

    public override void OnPick()
    {
        Player.PlayerHealthChanged += Player_PlayerHealthChanged;
    }

    public override void OnDrop()
    {
        Player.PlayerHealthChanged -= Player_PlayerHealthChanged;
    }

    private void Player_PlayerHealthChanged()
    {
        for (int i = 0; i < Player.PlayerWeapons.Length; ++i)
        {
            Player.PlayerWeapons[i].WeaponParameters.BPS *= 1.4f;
        }
    }
}