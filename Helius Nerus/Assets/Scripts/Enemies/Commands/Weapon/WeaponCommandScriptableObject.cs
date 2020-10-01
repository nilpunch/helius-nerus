using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCommand", menuName = "ScriptableObjects/Weapon commands", order = 3)]
public class WeaponCommandScriptableObject : ScriptableObject, ICommandParameters
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
