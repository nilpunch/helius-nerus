using System.Collections;
using UnityEngine;

public class DoubleShootModifier : IPlayerWeaponModifier
{
	public const float COLLISION_DISABLE_TIME = 0.1f;

	private WaitForSeconds _waiter = new WaitForSeconds(COLLISION_DISABLE_TIME);

	public IPlayerWeaponModifier Clone()
	{
		return (DoubleShootModifier)MemberwiseClone();
	}

	public void OnDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnBulletEnable(PlayerBullet playerBullet)
	{
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
		if (playerBullet.ProcedModifiers.Contains(ModifierType.DoubleShootModifier) == false)
		{
			playerBullet.BulletParameters.Durability += 1;
			playerBullet.ProcedModifiers.Add(ModifierType.DoubleShootModifier);
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
