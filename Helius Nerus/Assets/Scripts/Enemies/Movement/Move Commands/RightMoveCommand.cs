using UnityEngine;

public class RightMoveCommand : MoveCommand
{
    private float _timeElapsed = 0.0f;

    public RightMoveCommand()
    {
        _timeElapsed = 0.0f;
    }

    public RightMoveCommand(MoveCommandData commandData) : base(commandData)
    {
        _timeElapsed = 0.0f;
    }

    public override bool IsEnded()
    {
		return _timeElapsed > CommandData.EndParameter;
    }

    public override void Tick(Transform ship)
    {
        _timeElapsed += Time.deltaTime * CommandData.TimeScale;
        ship.Translate(Vector3.right * CommandData.MovementMultiplier * Time.deltaTime * CommandData.TimeScale, Space.World);
    }

    public override void Reset()
    {
        _timeElapsed = 0.0f;
    }
}
