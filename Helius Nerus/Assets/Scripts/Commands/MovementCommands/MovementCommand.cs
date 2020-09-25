using UnityEngine;

[System.Serializable]
public abstract class MovementCommand
{
    public bool WorkOnce
    {
        get => _workOnce;
    }
    [Tooltip("Множитель движения команды")]
    [SerializeField] protected float _movementMultiplier = 1.0f;
    [Tooltip("Множитель времени для команды")]
    [SerializeField] protected float _timeScale = 1.0f;
    [Tooltip("Параметр для определения завершения команды")]
    [SerializeField] protected float _endParameter = 1.0f;
    [Tooltip("Эта команда выполнится один раз?")]
    [SerializeField] protected bool _workOnce = false;

    public abstract void Tick(Transform ship);
    public abstract bool IsEnded();
    public abstract void Reset();

    public MovementCommand(float movementMult = 1.0f, float timeScale = 1.0f, float endParam = 1.0f, bool workOnce = false)
    {
        _movementMultiplier = movementMult;
        _timeScale = timeScale;
        _endParameter = endParam;
        _workOnce = workOnce;
    }

    public MovementCommand()
    {

    }

    public void SetParametrs(float movementMult = 1.0f, float timeScale = 1.0f, float endParam = 1.0f, bool workOnce = false)
    {
        _movementMultiplier = movementMult;
        _timeScale = timeScale;
        _endParameter = endParam;
        _workOnce = workOnce;
    }
}