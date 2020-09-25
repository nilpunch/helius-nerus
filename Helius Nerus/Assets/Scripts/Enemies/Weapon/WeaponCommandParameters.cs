using System;
using UnityEngine;

public class WeaponCommandParameters : ICommandParameters
{
	[SerializeField] private string _commandType = "Down";
	[SerializeField] private WeaponCommandData _weaponCommandData = null;

	public IEnemyCommand CommandParamsToCommand()
	{
		Type comType = Type.GetType(_commandType + "MovementCommand");
		WeaponCommand instance = (WeaponCommand)Activator.CreateInstance(comType);
		instance.SetParametrs(_weaponCommandData);

		return instance;
	}
}
