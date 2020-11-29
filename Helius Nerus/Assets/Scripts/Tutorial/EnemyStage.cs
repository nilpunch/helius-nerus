using System.Collections;
using UnityEngine;

public class EnemyStage : TutorialStage
{
    public EnemyStage(TutorialController controller) : base(controller)
    {
    }

    public override void StartStage()
    {
        _controller.StartCoroutine(StartCoroutine());
    }

    private IEnumerator StartCoroutine()
    {
        GameObject go = GameObject.Instantiate(_controller.TutorialEnemy.gameObject);
        go.transform.position = new Vector3(0f, 6f, 0f);

        yield return new WaitForSeconds(3f);

        _hintCanvas.SetText("This is your first enemy. It won't damage you on contact, but real one will. Kill it!");
        _hintCanvas.Show();

        // to sub
        yield break;
    }

    private IEnumerator EndCoroutine()
    {
        _hintCanvas.Hide();
        Player.Instance.IsNotShooting = false;
        Player.Instance.IsNotMoving = false;

        TutorialEnemy.TutorialEnemyDied += TutorialEnemy_TutorialEnemyDied;
        // to sub
        yield break;
    }

    private void TutorialEnemy_TutorialEnemyDied()
    {
        Player.Instance.IsNotShooting = true;
        Player.Instance.IsNotMoving = true;
        TutorialEnemy.TutorialEnemyDied -= TutorialEnemy_TutorialEnemyDied;
        _controller.StartNextStage();
    }

    public override void EndStage()
    {
        _controller.StartCoroutine(EndCoroutine());
    }
}
