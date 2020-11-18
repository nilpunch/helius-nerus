using UnityEngine;

public class MouseMoveInput : IMoveInput
{
	public const float THRUST_RADIUS = 0.5f;
	public const float MIN_THRUST = 0.1f;

	private float _sqrThrustRadius = 1f;
	private Transform _radiusOrigin = null;
	private float _minThrust = 0f;
	private float _noThrustDistance = 0f;

	private Camera _main = null;

	public Vector2 Direction { get; private set; }
	public float Thrust { get; private set; }

	public MouseMoveInput(Transform origin)
	{
		_radiusOrigin = origin;
		_sqrThrustRadius = THRUST_RADIUS;
		_minThrust = MIN_THRUST;
		_noThrustDistance = _minThrust * 0.001f;

		_main = Camera.main;
	}

	public void ReadInput()
	{
		Vector2 directionedDistance = _main.ScreenToWorldPoint(Input.mousePosition) - _radiusOrigin.position;
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
		Thrust *= 7f;
	}

	public void Tick(Transform transform)
	{
		transform.Translate((Vector3)Direction * Thrust * TimeManager.PlayerDeltaTime, Space.World);
	}
}
