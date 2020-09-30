using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateMovementCommand : MonoBehaviour
{
    private List<Type> _moveCommandTypes = new List<Type>();
    private List<MoveCommand> _moveCommands = new List<MoveCommand>();


    private void PreCookTypes()
    {
        _moveCommandTypes.Clear();
        foreach (MoveCommandType type in (MoveCommandType[])Enum.GetValues(typeof(MoveCommandType)))
        {
            Type ctype = Type.GetType(type.ToString() + "MoveCommand");
            _moveCommandTypes.Add(ctype);
            _moveCommands.Add((MoveCommand)Activator.CreateInstance(ctype));
        }
    }

    public void Initialize()
    {
        PreCookTypes();
    }

    public Type GetTypeByEnum(MoveCommandType type)
    {
        return _moveCommandTypes[(int)type];
    }

    public MoveCommand GetCommandByEnum(MoveCommandType type)
    {
        return _moveCommands[(int)type].Clone();
    }
}
