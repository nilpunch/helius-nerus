using System.Collections;
using System.Collections.Generic;
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

	public void OnEnable(PlayerBullet playerBullet)
	{
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public void OnTick(PlayerBullet playerBullet)
	{
		playerBullet.Transform.Translate(Vector3.up * Time.deltaTime * playerBullet.BulletParameters.SpeedMultiplier, Space.Self);
	}
}
