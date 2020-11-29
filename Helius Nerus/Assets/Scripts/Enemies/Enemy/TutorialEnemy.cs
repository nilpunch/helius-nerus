using DG.Tweening;

public class TutorialEnemy : Enemy
{
    public static event System.Action TutorialEnemyDied = delegate { };

    public override void Enabled()
    {
        transform.DOMoveY(3f, 3f);
    }

    public override void Disabled()
    {
        //base.Disabled();
        TutorialEnemyDied.Invoke();
    }
}
