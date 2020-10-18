using UnityEngine;

public class TransformMover
{
	public Vector2 LastDeltaMovement { get; private set; } = Vector2.zero;

	private readonly IMoveInput _moveInput;
	private readonly Transform _transform;
	private readonly MoveParameters _moveSettings;

	public TransformMover(IMoveInput moveInput, Transform transform, MoveParameters moveSettings)
	{
		_moveInput = moveInput;
		_transform = transform;
		_moveSettings = moveSettings;
	}

	public void Tick()
	{
		LastDeltaMovement = _moveInput.Direction * _moveInput.Thrust * _moveSettings.MoveSpeed * Time.deltaTime;
		_transform.Translate(LastDeltaMovement, Space.World);
	}
}
