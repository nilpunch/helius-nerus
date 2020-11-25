public class FullHPRewardArtifact : PlayerArtifact
{
    public override string MyEnumName => "FullHPRewardArtifact";

    public override PlayerArtifact Clone()
    {
        return (FullHPRewardArtifact)this.MemberwiseClone();
    }

    public override void OnPick()
    {
        LevelBoss.BossDied += LevelBoss_BossDied;
    }

    public override void OnDrop()
    {
        LevelBoss.BossDied -= LevelBoss_BossDied;
    }

    private void LevelBoss_BossDied()
    {
        if (Player.PlayerParameters.MaxHealth == Player.PlayerParameters.CurrentHealth)
            ScoreCounter.Instance.IncrementScore(10000);
    }
}
