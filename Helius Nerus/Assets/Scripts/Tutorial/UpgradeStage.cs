using System.Collections;

public class UpgradeStage : TutorialStage
{
    public UpgradeStage(TutorialController controller) : base(controller)
    {
    }

    public override void EndStage()
    {
        base.EndStage();
        _controller.StartCoroutine(EndCoroutine());
    }

    private IEnumerator EndCoroutine()
    {
        _hintCanvas.Hide();
        //Player.Instance.IsNotShooting = false;
        Player.Instance.IsNotMoving = false;

        ScoreCounter.ScoreWasUpdated += ScoreCounter_ScoreWasUpdated;
        // to sub
        yield break;
    }

    private void ScoreCounter_ScoreWasUpdated()
    {
        _controller.StartNextStage();
        ScoreCounter.ScoreWasUpdated -= ScoreCounter_ScoreWasUpdated;
    }

    public override void StartStage()
    {
        base.StartStage();
        _hintCanvas.SetText("This enemy dropped an upgrade. It will heal you for one heart, or give you 1000 points if your health are full. \n Pick it up!");
        _hintCanvas.Show();
    }
}