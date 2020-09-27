using UnityEngine;

class DelayWeaponCommand : WeaponCommand
{
	private float _timeElapsed = 0.0f;

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
		_timeElapsed += Time.deltaTime * CommandData.TimeScale;
	}
}