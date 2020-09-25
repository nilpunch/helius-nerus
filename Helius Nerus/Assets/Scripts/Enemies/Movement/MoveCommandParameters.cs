using System;
using UnityEngine;

[System.Serializable]
public class MoveCommandParameters : ICommandParameters
{
	[SerializeField] private string _commandType = "Down";
	[SerializeField] private MoveCommandData _moveCommandData = null;

	public IEnemyCommand CommandParamsToCommand()
	{
		Type comType = Type.GetType(_commandType + "MovementCommand");
		MoveCommand instance = (MoveCommand)Activator.CreateInstance(comType);
		instance.SetParametrs(_moveCommandData);
		return instance;
	}
}