public class FirstResurrectionAchievement : Achievment
{
    public override void Init(bool wasTriggered = false)
    {
        base.Init(wasTriggered);

        _achievementName = "resurrectionAchievementName";
        _achievementDescription = "resurrectionAchievementDescription";
        //_sprite = Resources.Load<Sprite>("SpritePath");
    }

    protected override void Subscribe()
    {
        Player.PlayerResurrection += AchievmentHappened;
    }

    protected override void Unsubscribe()
    {
        Player.PlayerResurrection -= AchievmentHappened;
    }
}
