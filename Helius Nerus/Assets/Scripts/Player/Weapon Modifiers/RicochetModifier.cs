using System.Collections;
using UnityEngine;

public class RicochetModifier : PlayerWeaponModifier
{
	public const float COLLISION_DISABLE_TIME = 0.1f;
	public const float RICOCHET_ANGLE = 90f;

	private WaitForSeconds _waiter = new WaitForSeconds(COLLISION_DISABLE_TIME);

    public override string MyEnumName => "RicochetModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (RicochetModifier)MemberwiseClone();
	}

	public override void OnBulletEnable(PlayerBullet playerBullet)
	{
		if (playerBullet.ModifiersProcCount.ContainsKey(ModifierType.RicochetModifier) == false)
		{
			playerBullet.ModifiersProcCount.Add(ModifierType.RicochetModifier, 1);
		}
		else
		{
			playerBullet.ModifiersProcCount[ModifierType.RicochetModifier] += 1;
		}
	}

	public override void OnHit(PlayerBullet playerBullet, GameObject target)
	{
		if (playerBullet.ModifiersProcCount[ModifierType.RicochetModifier] > 0)
		{
			playerBullet.ModifiersProcCount[ModifierType.RicochetModifier] -= 1;
			playerBullet.BulletParameters.Durability += 1;
			playerBullet.Transform.Rotate(0f, 0f, Random.Range(-RICOCHET_ANGLE, RICOCHET_ANGLE));
			CoroutineProcessor.Instance.StartCoroutine(OnBulletProc(playerBullet));
		}
	}

	public IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		playerBullet.CollideWithEnemies = false;
		yield return _waiter;
		playerBullet.CollideWithEnemies = true;
	}
}
