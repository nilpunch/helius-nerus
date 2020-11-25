﻿using System.Collections;
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

    protected static void Shoot(WeaponCommandData commandData)
    {
        float _halfBulletAmount = 0f;
        float _angleStep = 0f;

        if (commandData.BulletAmount > 1)
        {
            _halfBulletAmount = (commandData.BulletAmount - 1) / 2f;
            _angleStep = commandData.SpreadAngle / _halfBulletAmount / 2f;
        }
        else
        {
            _halfBulletAmount = 0f;
            _angleStep = 0f;
        }

        for (int i = 0; i < commandData.BulletAmount; ++i)
        {
            GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(commandData.BulletType);
            (bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = commandData.BulletSpeed;
            bullet.transform.position = commandData.Position;
            bullet.transform.localScale = commandData.BulletSize * Vector3.one;
            bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, commandData.Direction) + (_angleStep * (i - _halfBulletAmount)));
        }
    }

    protected static void Shoot(int bulletAmount = 1, float spreadAngle = 10f, float bulletSpeed = 7f, 
                                BulletTypes bulletType = BulletTypes.StraightMove,
                                Vector3? position = null, float bulletSize = 0.3f,
                                Vector2? direction = null)
    {
        if (position.HasValue == false)
            position = Boss.Instance.transform.position;
        if (direction.HasValue == false)
            direction = Vector2.down;

        float _halfBulletAmount = 0f;
        float _angleStep = 0f;

        if (bulletAmount > 1)
        {
            _halfBulletAmount = (bulletAmount - 1) / 2f;
            _angleStep = spreadAngle / _halfBulletAmount / 2f;
        }
        else
        {
            _halfBulletAmount = 0f;
            _angleStep = 0f;
        }

        for (int i = 0; i < bulletAmount; ++i)
        {
            GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(bulletType);
            (bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = bulletSpeed;
            bullet.transform.position = position.Value;
            bullet.transform.localScale = bulletSize * Vector3.one;
            bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, direction.Value) + (_angleStep * (i - _halfBulletAmount)));
        }
    }
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

        Shoot();

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

        Shoot();

        NotifyBossPhase();
        yield break;
    }
}

