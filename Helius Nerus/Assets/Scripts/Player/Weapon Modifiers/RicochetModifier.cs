using System.Collections;
using UnityEngine;

public class RicochetModifier : IPlayerWeaponModifier
{
	public const float COLLISION_DISABLE_TIME = 0.1f;

	private WaitForSeconds _waiter = new WaitForSeconds(COLLISION_DISABLE_TIME);

	public IPlayerWeaponModifier Clone()
	{
		return (RicochetModifier)MemberwiseClone();
	}

	public void OnBulletDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnBulletEnable(PlayerBullet playerBullet)
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

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
		if (playerBullet.ModifiersProcCount[ModifierType.RicochetModifier] > 0)
		{
			playerBullet.ModifiersProcCount[ModifierType.RicochetModifier] -= 1;
			playerBullet.BulletParameters.Durability += 1;
			playerBullet.Transform.Rotate(0f, 0f, Random.Range(0f, 360f));
			CoroutineProcessor.ProcessModifier(this, playerBullet);
		}
	}

	public IEnumerator OnProc(PlayerBullet playerBullet)
	{
		playerBullet.CollideWithEnemies = false;
		yield return _waiter;
		playerBullet.CollideWithEnemies = true;
	}

	public void OnPick(PlayerWeapon playerWeapon)
	{
	}

	public void OnDrop(PlayerWeapon playerWeapon)
	{
	}

	public void OnWeaponShoot(PlayerWeapon playerBullet)
	{
	}
}
