using UnityEngine;

class DelayMoveCommand : MoveCommand
{
	private float _timeElapsed = 0.0f;

    public DelayMoveCommand()
    {

    }

	public override bool IsEnded()
	{
		return _timeElapsed > CommandData.EndParameter;
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