public class ResurrectArtifact : PlayerArtifact
{
    public override string MyEnumName => "ResurrectArtifact";

    public override ArtifactType MyEnum => ArtifactType.ResurrectArtifact;

    public override PlayerArtifact Clone()
    {
        return (ResurrectArtifact)this.MemberwiseClone();
    }

    public override void OnPick()
    {
        Player.PlayerBeforeDie += Player_PlayerBeforeDie;
    }

    private void Player_PlayerBeforeDie()
    {
        Player.PlayerParameters.CurrentHealth = 1;
        // unsub?
        //remove art
        OnDrop();
        Player.PlayerArtifacts.Remove(this);
        Player.Instance.AddArtifact(PlayerArtifactContainer.Instance.GetArtifact(ArtifactType.UsedResurrectArtifact));
    }

    public override void OnDrop()
    {
        Player.PlayerBeforeDie -= Player_PlayerBeforeDie;
    }
}
