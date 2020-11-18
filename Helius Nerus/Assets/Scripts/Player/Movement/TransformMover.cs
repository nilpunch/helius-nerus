using UnityEngine;

public class TransformMover
{
	public const float DESCTOP_MAX_SPEED = 6f;

	private readonly IMoveInput _moveInput;
	private readonly Transform _transform;
	private readonly float _sensivity;

	public TransformMover(IMoveInput moveInput, Transform transform, float sensivity)
	{
		_moveInput = moveInput;
		_transform = transform;
		_sensivity = sensivity;
	}

	public void Tick()
	{
		_transform.Translate((Vector3)_moveInput.Direction * _moveInput.Thrust *
            _sensivity * TimeManager.PlayerDeltaTime, Space.World);
	}
}
