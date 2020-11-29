public class UIStage : TutorialStage
{
    public UIStage(TutorialController controller) : base(controller)
    {
    }

    public override void StartStage()
    {
        base.StartStage();
        _hintCanvas.SetText("Welcome to tutorial. \n At the top of the screen, you can see your score (right), ship artifacts(left) and health (blue hearts). Press OK to continue");
        _hintCanvas.Show();

        Player.Instance.IsNotMoving = true;
        Player.Instance.IsNotShooting = true;
    }

    public override void EndStage()
    {
        _hintCanvas.Hide();
        _controller.StartNextStage();
    }
}
