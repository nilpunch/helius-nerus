using System;
using UnityEngine;

public class StraightShootWeaponCommand : WeaponCommand
{
	private float _timeElapsed = 0.0f;
	private bool _isShootedOnce = false;

	public StraightShootWeaponCommand()
	{
		_timeElapsed = 0.0f;
	}

	public StraightShootWeaponCommand(WeaponCommandData commandData) : base(commandData)
	{
		_timeElapsed = 0.0f;
	}

	public override bool IsEnded()
	{
		return _timeElapsed > CommandData.DelayBeforeShoot + CommandData.DelayAfterShoot;
	}

	public override void Reset()
	{
		_timeElapsed = 0.0f;
	}

	public override void Tick(Transform transform)
	{
		_timeElapsed += Time.deltaTime;
		// TODO
	}
}