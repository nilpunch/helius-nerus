using System;
using UnityEngine;

[System.Serializable]
public class WeaponCommandParameters : ICommandParameters
{
	[SerializeField] private WeaponCommandType _shootType = WeaponCommandType.Delay;
	[SerializeField] private WeaponCommandData _weaponCommandData = null;

	public IEnemyCommand CreateCommand()
	{
		WeaponCommand instance = CommandsCollection.GetCommandByEnum(_shootType);
		instance.SetParametrs(_weaponCommandData);
		return instance;
	}
}
