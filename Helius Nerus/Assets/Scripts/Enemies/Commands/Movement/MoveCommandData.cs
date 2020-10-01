using System;
using UnityEngine;

[System.Serializable]
public class MoveCommandData
{
	[Tooltip("Множитель движения команды")]
	[SerializeField] private float _movementMultiplier = 1.0f;
	[Tooltip("Множитель времени для команды")]
	[SerializeField] private float _timeScale = 1.0f;
	[Tooltip("Параметр для определения завершения команды")]
	[SerializeField] private float _endParameter = 1.0f;
	[Tooltip("Эта команда выполнится один раз?")]
	[SerializeField] private bool _workOnce = false;

	public float MovementMultiplier { get => _movementMultiplier; set => _movementMultiplier = value; }
	public float TimeScale { get => _timeScale; set => _timeScale = value; }
	public float EndParameter { get => _endParameter; set => _endParameter = value; }
	public bool WorkOnce { get => _workOnce; set => _workOnce = value; }

    // Ктор
    public MoveCommandData(float movement, float time, float end, bool work)
    {
        _movementMultiplier = movement;
        _timeScale = time;
        _endParameter = end;
        _workOnce = work;
    }

    public MoveCommandData Clone()
    {
        return (MoveCommandData)this.MemberwiseClone();
    }
}