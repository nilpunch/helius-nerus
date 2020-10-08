using System;
using System.Collections.Generic;
using UnityEngine;

public enum MoveCommandType
{
	Delay,
	Horizontal,
	Vertical,
}

public enum WeaponCommandType
{
	Delay,
	StraightShoot,
}

public class CommandsCollection : MonoBehaviour
{
	public static CommandsCollection Instance { get; private set; } = null;

	private List<Type> _moveCommandTypes = new List<Type>();
    private List<MoveCommand> _moveCommands = new List<MoveCommand>();
	private List<Type> _weaponCommandTypes = new List<Type>();
	private List<WeaponCommand> _weaponCommands = new List<WeaponCommand>();

    public static Type GetTypeByEnum(MoveCommandType type)
    {
        return Instance._moveCommandTypes[(int)type];
    }
    public static MoveCommand GetCommandByEnum(MoveCommandType type)
    {
        return Instance._moveCommands[(int)type].Clone();
    }
	public MoveCommand this[MoveCommandType type]
	{
		get => _moveCommands[(int)type].Clone();
	}

	public static Type GetTypeByEnum(WeaponCommandType type)
	{
		return Instance._weaponCommandTypes[(int)type];
	}
	public static WeaponCommand GetCommandByEnum(WeaponCommandType type)
	{
		return Instance._weaponCommands[(int)type].Clone();
	}
	public WeaponCommand this[WeaponCommandType type]
	{
		get => _weaponCommands[(int)type].Clone();
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		PreCookTypes();
    }

    private void PreCookTypes()
    {
        _moveCommandTypes.Clear();
		_moveCommands.Clear();
		foreach (MoveCommandType type in (MoveCommandType[])Enum.GetValues(typeof(MoveCommandType)))
        {
            Type ctype = Type.GetType(type.ToString() + "MoveCommand");
#if UNITY_EDITOR
			if (ctype == null)
			{
				Debug.LogError("CommandsCollection handle wrong move command name: " + type.ToString() + "MoveCommand");
				Debug.Break();
			}
#endif
			_moveCommandTypes.Add(ctype);
            _moveCommands.Add((MoveCommand)Activator.CreateInstance(ctype));
        }

		_weaponCommandTypes.Clear();
		_weaponCommands.Clear();
		foreach (WeaponCommandType type in (WeaponCommandType[])Enum.GetValues(typeof(WeaponCommandType)))
		{
			Type ctype = Type.GetType(type.ToString() + "WeaponCommand");
#if UNITY_EDITOR
			if (ctype == null)
			{
				Debug.LogError("CommandsCollection handle wrong weapon command name: " + type.ToString() + "WeaponCommand");
				Debug.Break();
			}
#endif
			_weaponCommandTypes.Add(ctype);
			_weaponCommands.Add((WeaponCommand)Activator.CreateInstance(ctype));
		}
	}
}
