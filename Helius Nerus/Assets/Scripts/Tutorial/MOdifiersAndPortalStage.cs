using UnityEngine;

public class MOdifiersAndPortalStage : TutorialStage
{
    public MOdifiersAndPortalStage(TutorialController controller) : base(controller)
    {
    }

    public override void EndStage()
    {
        base.EndStage();
        _hintCanvas.Hide();
        PlayerPrefs.SetFloat("Tutorial", 1);
        PlayerPrefs.Save();
        ScoreCounter.Instance.Reset();
        _controller.StartNextStage();
    }

    public override void StartStage()
    {
        _hintCanvas.SetText("After you killed all the level enemies, a portal will appear. Go through the portal, and you will choose a modifiers for both of your weapons. Drag modifier onto a weapon slot, and you will proceed to next level. Press OK to leave tutorial");
        _hintCanvas.Show();
        Player.Instance.IsNotMoving = true;
    }
}
