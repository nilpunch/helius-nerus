using System.Collections;
using UnityEngine;

public class ShootingStage : TutorialStage
{
    public ShootingStage(TutorialController controller) : base(controller)
    {
    }

    public override void StartStage()
    {
        _hintCanvas.Show();
        _hintCanvas.SetText("Your ship shoots automatically all the time. Press OK to see shooting.");
    }

    private IEnumerator Coroutine()
    {
        _hintCanvas.Hide();
        Player.Instance.IsNotShooting = false;
        yield return new WaitForSeconds(3.0f);
        Player.Instance.IsNotShooting = true;
        _controller.StartNextStage();
        yield break;
    }


    public override void EndStage()
    {
        _controller.StartCoroutine(Coroutine());
    }
}
