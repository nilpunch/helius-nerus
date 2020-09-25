using UnityEngine;

public class RightMovementCommand : MovementCommand
{
    private float _timeElapsed = 0.0f;

    public RightMovementCommand(float movementMult, float timeScale, float endParam, bool workOnce) : base(movementMult, timeScale, endParam, workOnce)
    {
        _timeElapsed = 0.0f;
    }

    public RightMovementCommand()
    {
        _timeElapsed = 0.0f;
    }

    public override bool IsEnded()
    {
        if (_timeElapsed > _endParameter)
            return true;
        return false;
    }

    public override void Tick(Transform ship)
    {
        _timeElapsed += Time.deltaTime * _timeScale;
        ship.Translate(Vector3.right * _movementMultiplier * Time.deltaTime * _timeScale, Space.World);
    }

    public override void Reset()
    {
        _timeElapsed = 0.0f;
    }
}
