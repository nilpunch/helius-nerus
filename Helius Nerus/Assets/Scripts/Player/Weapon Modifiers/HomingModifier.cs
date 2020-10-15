using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingModifier : IPlayerWeaponModifier
{
	public IPlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}

	public void OnDestroy(PlayerBullet playerBullet)
	{
		throw new System.NotImplementedException();
	}

	public void OnEnable(PlayerBullet playerBullet)
	{
		throw new System.NotImplementedException();
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}

	public void OnTick(PlayerBullet playerBullet)
	{
		throw new System.NotImplementedException();
	}
}
