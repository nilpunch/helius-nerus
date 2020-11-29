using System.Collections;
using UnityEngine;


public class ShootBulletsIn8Direction : BossAction
{
	private float _moveTime = 2.0f;
	private float _shootTime = 0.2f;

	protected override IEnumerator Action()
	{
		float timeElapsed = 0.0f;
		float shootTimeElapsed = 0.0f;
		Vector3 startPos = Boss.Instance.gameObject.transform.position;
		Vector3 finalPos = startPos.With(x: -ParallaxCamera.ParallaxSize.x / 2 + 1);


		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot();
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPos, timeElapsed / _moveTime);
			yield return null;
		}

		NotifyBossPhase();
		yield break;
	}
}

public class MoveLeftSpikeBossAction : BossAction
{
	private float _moveTime = 2.0f;
	private float _shootTime = 0.2f;

	protected override IEnumerator Action()
	{
		float timeElapsed = 0.0f;
		float shootTimeElapsed = 0.0f;
		Vector3 startPos = Boss.Instance.gameObject.transform.position;
		Vector3 finalPos = startPos.With(x: -ParallaxCamera.ParallaxSize.x / 2 + 1);


		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot();
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPos, timeElapsed / _moveTime);
			yield return null;
		}

		NotifyBossPhase();
		yield break;
	}
}
public class MoveRightSpikeBossAction : BossAction
{
	private float _moveTime = 2.0f;
	private float _shootTime = 0.2f;

	protected override IEnumerator Action()
	{
		float timeElapsed = 0.0f;
		float shootTimeElapsed = 0.0f;
		Vector3 startPos = Boss.Instance.gameObject.transform.position;
		Vector3 finalPos = startPos.With(x: ParallaxCamera.ParallaxSize.x / 2 - 1);

		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot();
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPos, timeElapsed / _moveTime);
			yield return null;
		}

		Shoot();

		NotifyBossPhase();
		yield break;
	}
}

public class MoveUpAndDownWithShotgunSpikeBossAction : BossAction
{
	private float _moveTime = 1.0f;
	private float _shootTime = 0.4f;

	protected override IEnumerator Action()
	{
		float timeElapsed = 0.0f;
		float shootTimeElapsed = 0.0f;
		Vector3 startPos = Boss.Instance.gameObject.transform.position;
		Vector3 finalPosTop = startPos.With(y: startPos.y - 1);
		Vector3 finalPosBot = startPos.With(y: startPos.y + 1);

		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot(bulletAmount: 8, spreadAngle: 55, bulletSize: 0.2f);
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(startPos, finalPosTop, timeElapsed / _moveTime);
			yield return null;
		}
		timeElapsed = 0.0f;
		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot(bulletAmount: 8, spreadAngle: 55, bulletSize: 0.2f);
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(finalPosTop, finalPosBot, timeElapsed / _moveTime);
			yield return null;
		}
		timeElapsed = 0.0f;
		while (timeElapsed <= _moveTime)
		{
			timeElapsed += TimeManager.EnemyDeltaTime;
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot(bulletAmount: 8, spreadAngle: 55, bulletSize: 0.2f);
			}
			Boss.Instance.gameObject.transform.position = Vector3.Lerp(finalPosBot, startPos, timeElapsed / _moveTime);
			yield return null;
		}
		NotifyBossPhase();
		yield break;
	}
}

public class StayStillAndSpraySpikeBossAction : BossAction
{
	private float _shootTime = 0.05f;

	protected override IEnumerator Action()
	{
		float shootTimeElapsed = 0.0f;
		float timeElapsed = 0.0f;
		float angle = 0.0f;
		Vector3 startPos = Boss.Instance.gameObject.transform.position;
		Vector3 finalPos = startPos.With(x: ParallaxCamera.ParallaxSize.x / 2 - 1);

		while (true)
		{
			shootTimeElapsed += TimeManager.EnemyDeltaTime;
			timeElapsed += TimeManager.EnemyDeltaTime * 2;
			angle = Mathf.Sin(timeElapsed);
			if (shootTimeElapsed > _shootTime)
			{
				shootTimeElapsed = 0.0f;
				Shoot(direction: new Vector2(angle, -1), bulletSize: 0.1f);
			}
			yield return null;
		}

		//Shoot();

		//NotifyBossPhase();
		//yield break;
	}
}