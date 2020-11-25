public class IncreaseHPAndHealArtifact : PlayerArtifact
{
    public override string MyEnumName => "IncreaseHPAndHealArtifact";

    public override PlayerArtifact Clone()
    {
        return (IncreaseHPAndHealArtifact)this.MemberwiseClone();
    }

    public override void OnPick()
    {
        Player.PlayerParameters.MaxHealth += 1;
        Player.PlayerParameters.CurrentHealth = Player.PlayerParameters.MaxHealth;
    }
}
