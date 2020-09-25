using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : IEnemyWeapon
{
	public void Shoot(WeaponCommandData commandData)
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
			GameObject bullet = GameObject.Instantiate(commandData.BulletPrefab);
			bullet.transform.position = commandData.Position;
			bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, commandData.Direction) + _angleStep * (i - _halfBulletAmount));
			bullet.SetActive(true);
		}
	}
}
