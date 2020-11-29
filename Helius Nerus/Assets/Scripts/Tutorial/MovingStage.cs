using System.Collections;
using UnityEngine;

public class MovingStage : TutorialStage
{
    public MovingStage(TutorialController controller) : base(controller)
    {
    }

    public override void StartStage()
    {
        _hintCanvas.SetText("You can move your ship across the screen using your finger Move it aroung a little bit. Press OK to close this window.");
        _hintCanvas.Show();
    }

    private IEnumerator Coroutine()
    {
        _hintCanvas.Hide();
        Player.Instance.IsNotMoving = false;
        yield return new WaitForSeconds(5.0f);
        Player.Instance.IsNotMoving = true;
        _controller.StartNextStage();
        yield break;
    }


    public override void EndStage()
    {
        _controller.StartCoroutine(Coroutine());
    }
}
