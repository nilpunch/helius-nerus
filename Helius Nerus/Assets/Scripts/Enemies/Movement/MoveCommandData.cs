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

	public float MovementMultiplier => _movementMultiplier;
	public float TimeScale => _timeScale;
	public float EndParameter => _endParameter;
	public bool WorkOnce => _workOnce;
}