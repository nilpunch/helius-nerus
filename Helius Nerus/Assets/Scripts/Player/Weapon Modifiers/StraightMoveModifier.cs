using System.Collections;
using UnityEngine;

public class StraightMoveModifier : IPlayerWeaponModifier
{
	public IPlayerWeaponModifier Clone()
	{
		return (StraightMoveModifier)MemberwiseClone();
	}

	public void OnDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnShoot(PlayerBullet playerBullet)
	{
		CoroutineProcessor.ProcessModifier(this, playerBullet);
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public IEnumerator OnProc(PlayerBullet playerBullet)
	{
		while (playerBullet.gameObject.activeInHierarchy)
		{
			playerBullet.Transform.Translate(Vector3.up * Time.deltaTime * playerBullet.BulletParameters.SpeedMultiplier, Space.Self);
			yield return null;
		}
	}
}
