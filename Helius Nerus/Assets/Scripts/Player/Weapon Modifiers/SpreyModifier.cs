using System.Collections;
using UnityEngine;

public class SpreyModifier : IPlayerWeaponModifier
{
	public const float WEAPON_MOVE_TIME = 1f;

	public IPlayerWeaponModifier Clone()
	{
		return (SpreyModifier)MemberwiseClone();
	}

	public void OnBulletDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnBulletEnable(PlayerBullet playerBullet)
	{
	}

	public void OnDrop(PlayerWeapon playerWeapon)
	{
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public void OnPick(PlayerWeapon playerWeapon)
	{
		playerWeapon.StartCoroutine(OnWeaponProc(playerWeapon));
	}

	public IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		yield break;
	}

	public IEnumerator OnWeaponProc(PlayerWeapon playerWeapon)
	{
		float startWeaponAngle = playerWeapon.WeaponParameters.WeaponAngle;
		float offsetAngle = 0f;
		bool goBack = false;

		while (true)
		{
			if (Pause.Paused)
			{
				yield return null;
				continue;
			}

			if (playerWeapon.IsNoSooting)
			{
				playerWeapon.WeaponParameters.WeaponAngle = startWeaponAngle;
				yield return null;
				continue;
			}

			float deltaAngle = playerWeapon.WeaponParameters.SpreadAngle * Time.deltaTime * WEAPON_MOVE_TIME;
			if (goBack == false)
			{
				if (offsetAngle < playerWeapon.WeaponParameters.SpreadAngle / 2f)
				{
					offsetAngle += deltaAngle;
				}
				else
				{
					goBack = true;
				}
			}
			else
			{
				if (offsetAngle > -playerWeapon.WeaponParameters.SpreadAngle / 2f)
				{
					offsetAngle -= deltaAngle;
				}
				else
				{
					goBack = false;
				}
			}
			playerWeapon.WeaponParameters.WeaponAngle = startWeaponAngle + offsetAngle;
			yield return null;
		}
	}

	public void OnWeaponShoot(PlayerWeapon playerWeapon)
	{
	}
}