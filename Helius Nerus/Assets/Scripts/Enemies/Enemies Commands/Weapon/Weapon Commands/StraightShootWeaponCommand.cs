using System;
using UnityEngine;

public class StraightShootWeaponCommand : WeaponCommand
{
	private float _timeElapsed = 0.0f;
	private bool _isShootedOnce = false;

	public override bool IsEnded()
	{
		return _timeElapsed > CommandData.DelayBeforeShoot + CommandData.DelayAfterShoot;
	}

	public override void Reset()
	{
		_timeElapsed = 0.0f;
		_isShootedOnce = false;
		CommandData.RestoreData();
	}

	public override void Tick(Transform transform)
	{
		_timeElapsed += Time.deltaTime * CommandData.TimeScale;
		if (_timeElapsed > CommandData.DelayBeforeShoot && _isShootedOnce == false)
		{
			_isShootedOnce = true;
			CommandData.Position = CommandData.Position + (Vector2)transform.position;
			base.Shoot(CommandData);
		}
	}
}