using UnityEngine;

public class DownMovementCommand : MovementCommand
{
    private float _timeElapsed = 0.0f;

    public DownMovementCommand(float movementMult, float timeScale, float endParam, bool workOnce) : base(movementMult, timeScale, endParam, workOnce)
    {
        _timeElapsed = 0.0f;
    }

    public DownMovementCommand()
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
        ship.Translate(Vector3.down * _movementMultiplier * Time.deltaTime * _timeScale, Space.World);
    }

    public override void Reset()
    {
        _timeElapsed = 0.0f;
    }
}