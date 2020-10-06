using UnityEngine;

public class MouseMoveInput : IMoveInput
{
	public Vector2 Direction { get; private set; }
	public float Thrust { get; private set; }

	private float _sqrThrustRadius = 1f;
	private Transform _radiusOrigin = null;
	private float _minThrust = 0f;
	private float _noThrustDistance = 0f;

	private Camera _main = null;

	public MouseMoveInput(Transform origin, MouseMoveSettings mouseMoveSettings)
	{
		_radiusOrigin = origin;
		_sqrThrustRadius = mouseMoveSettings.ThrustRadius;
		_minThrust = mouseMoveSettings.MinThrust;
		_noThrustDistance = _minThrust * 0.001f;

		_main = Camera.main;
	}

	public void ReadInput()
	{
		Vector2 directionedDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _radiusOrigin.position;
		float sqrDistance = directionedDistance.sqrMagnitude;
		if (sqrDistance > _noThrustDistance)
		{
			Thrust = Mathf.SmoothStep(0.0f, 1.0f, Mathf.Clamp01(sqrDistance / _sqrThrustRadius + _minThrust));
		}
		else
		{
			Thrust = 0f;
		}
		Direction = directionedDistance.normalized;
	}
}
