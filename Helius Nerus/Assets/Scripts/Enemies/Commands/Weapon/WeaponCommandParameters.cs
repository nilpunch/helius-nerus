using System;
using UnityEngine;

[System.Serializable]
public class WeaponCommandParameters : ICommandParameters
{
	[SerializeField] private string _commandType = "Empty";
	[SerializeField] private WeaponCommandData _weaponCommandData = null;

	public IEnemyCommand CommandParamsToCommand()
	{
		Type comType = Type.GetType(_commandType + "WeaponCommand");
		if (comType != null)
		{
			WeaponCommand instance = (WeaponCommand)Activator.CreateInstance(comType);
			instance.SetParametrs(_weaponCommandData);
			return instance;
		}
		else
		{
#if UNITY_EDITOR
			Debug.LogWarning("WeaponCommandParameters handle wrong command type: " + _commandType + "WeaponCommand");
#endif
			return new EmptyEnemyCommand();
		}
	}
}
