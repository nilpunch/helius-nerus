using System;
using UnityEngine;

[System.Serializable]
public class MoveCommandParameters : ICommandParameters
{
	[SerializeField] private MoveCommandType _moveType = MoveCommandType.Delay;
	[SerializeField] private MoveCommandData _moveCommandData = null;

	public IEnemyCommand CreateCommand()
	{
		MoveCommand instance = CommandsCollection.GetCommandByEnum(_moveType);
		instance.SetParametrs(_moveCommandData);
		return instance;
	}
}