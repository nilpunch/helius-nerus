using System.Collections;
using UnityEngine;

public abstract class BossAction
{
    // for changing
    public BossPhase BossPhase
    {
        get;
        set;
    } = null;

    protected Coroutine _coroutine;
    public virtual void StartAction()
    {
        _coroutine = Boss.Instance.StartCoroutine(Action());
    }
    public virtual void StopAction()
    {
        if (_coroutine != null)
        {
            Boss.Instance.StopCoroutine(_coroutine);
        }
    }

    protected void NotifyBossPhase()
    {
        BossPhase.ChangeAction();
    }

    // NotifyBossPhase();
    // yield break;
    // Should be at the end
    protected abstract IEnumerator Action();
}

public class MoveLeftBossAction : BossAction
{
    private float _moveTime = 2.0f;

    protected override IEnumerator Action()
    {
        float timeElapsed = 0.0f;
        Vector3 startPos = Boss.Instance.gameObject.transform.position;
        Vector3 finalPos = startPos.With(x: -ParallaxCamera.ParallaxSize.x / 2 + 1);

        while (timeElapsed <= _moveTime)
        {
            timeElapsed += TimeManager.EnemyDeltaTime;
            Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPos, timeElapsed / _moveTime);
            yield return null;
        }

        NotifyBossPhase();
        yield break;
    }
}
public class MoveRightBossAction : BossAction
{
    private float _moveTime = 2.0f;

    protected override IEnumerator Action()
    {
        float timeElapsed = 0.0f;
        Vector3 startPos = Boss.Instance.gameObject.transform.position;
        Vector3 finalPos = startPos.With(x: ParallaxCamera.ParallaxSize.x / 2 - 1);

        while (timeElapsed <= _moveTime)
        {
            timeElapsed += TimeManager.EnemyDeltaTime;
            Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPos, timeElapsed / _moveTime);
            yield return null;
        }

        NotifyBossPhase();
        yield break;
    }
}

