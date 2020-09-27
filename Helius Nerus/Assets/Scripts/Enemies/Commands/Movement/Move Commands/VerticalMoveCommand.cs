using UnityEngine;

public class VerticalMoveCommand : MoveCommand
{
	private float _timeElapsed = 0.0f;

	public override bool IsEnded()
	{
		return _timeElapsed > CommandData.EndParameter;
	}

	public override void Reset()
	{
		_timeElapsed = 0.0f;
	}

	public override void Tick(Transform ship)
	{
		_timeElapsed += Time.deltaTime * CommandData.TimeScale;
		ship.Translate(Vector3.down * CommandData.MovementMultiplier * Time.deltaTime * CommandData.TimeScale, Space.World);
	}
}
