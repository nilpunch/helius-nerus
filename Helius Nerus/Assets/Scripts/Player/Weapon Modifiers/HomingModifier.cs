using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingModifier : PlayerWeaponModifier
{
	public const float MIN_TIME_SEARCH = 0.5f;
	public const float HOMING_COEFFICIENT = 50f;

    public override string MyEnumName => "HomingModifier";

	private WaitForSeconds _waitTime = new WaitForSeconds(MIN_TIME_SEARCH);

    public override PlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}

	public override void OnBulletEnable(PlayerBullet playerBullet)
	{
		base.OnBulletEnable(playerBullet);
		playerBullet.StartCoroutine(Homing(playerBullet));
	}

	private IEnumerator Homing(PlayerBullet playerBullet)
	{
		Transform target = null;

		while (playerBullet.gameObject.activeSelf)
		{
			if (target == null)
			{
				target = FindNearestEnemy(playerBullet);
				if (target == null)
				{
					yield return _waitTime;
					continue;
				}
			}
			
			if (target.gameObject.activeSelf == false)
			{
				target = null;
				yield return null;
				continue;
			}

			Vector3 direction = target.position - playerBullet.Transform.position;
			direction.Normalize();
			float rotation = Vector3.Cross(direction, playerBullet.Transform.up).z;

			float homingCoefficient = Mathf.Abs(rotation) > 0.01f ? HOMING_COEFFICIENT : 0f;

			playerBullet.Transform.localEulerAngles += new Vector3(0f, 0f,
				-Mathf.Sign(rotation) * homingCoefficient * TimeManager.PlayerDeltaTime);
			yield return null;
		}

	}

	private Transform FindNearestEnemy(PlayerBullet playerBullet)
	{
		List<GameObject> enemies = EnemyPoolContainer.Instance.GetAllEnemies();

		if (enemies.Count == 0)
		{
			return null;
		}

		int nearestEnemyIndex = 0;
		float minSqrDistance = (enemies[0].transform.position - playerBullet.Transform.position).sqrMagnitude;
		for (int i = 1; i < enemies.Count; i++)
		{
			if (enemies[i].activeSelf == false)
			{
				continue;
			}

			float sqrDistance = (enemies[i].transform.position - playerBullet.Transform.position).sqrMagnitude;
			if (sqrDistance < minSqrDistance)
			{
				minSqrDistance = sqrDistance;
				nearestEnemyIndex = i;
			}
		}

		return enemies[nearestEnemyIndex].transform;
	}
}
