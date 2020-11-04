public class DeathAchievement : Achievment
{
    public override void Init(bool wasTriggered = false)
    {
        base.Init(wasTriggered);

        _achievementName = "deathAchievementName";
        _achievementDescription = "deathAchievementDescription";
        //_sprite = Resources.Load<Sprite>("path");
    }

    protected override void Subscribe()
    {
        Player.PlayerDie += AchievmentHappened;
    }

    protected override void Unsubscribe()
    {
        Player.PlayerDie -= AchievmentHappened;
    }
}