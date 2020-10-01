using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommand", menuName = "ScriptableObjects/Move commands", order = 2)]
public class MoveCommandScriptableObject : ScriptableObject, ICommandParameters
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