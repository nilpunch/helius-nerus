using System;
using UnityEngine;

[System.Serializable]
public class MoveCommandParameters : ICommandParameters
{
	[SerializeField] private string _commandType = "Empty";
	[SerializeField] private MoveCommandData _moveCommandData = null;

	public IEnemyCommand CommandParamsToCommand()
	{
		Type comType = Type.GetType(_commandType + "MoveCommand");
		if (comType != null)
		{
			MoveCommand instance = (MoveCommand)Activator.CreateInstance(comType);
			instance.SetParametrs(_moveCommandData);
			return instance;
		}
		else
		{
#if UNITY_EDITOR
			Debug.LogWarning("MoveCommandParameters handle wrong command type: " + _commandType + "MoveCommand");
#endif
			return new EmptyEnemyCommand();
		}
	}
}