using UnityEngine;

public class EnemyOnCommands : Enemy
{
    [SerializeField] private CommandsProcessor<MoveCommandScriptableObject> _moveProcessor = new CommandsProcessor<MoveCommandScriptableObject>();
    [SerializeField] private CommandsProcessor<WeaponCommandScriptableObject> _weaponProcessor = new CommandsProcessor<WeaponCommandScriptableObject>();

    public override void OnUpdate()
    {
        _moveProcessor.TickCommandThreads();
        _weaponProcessor.TickCommandThreads();
    }

    public override void OnReset()
    {
        _moveProcessor.Initialize(transform);
        _weaponProcessor.Initialize(transform);
    }
}