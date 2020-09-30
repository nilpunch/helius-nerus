using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private CommandsProcessor<MoveCommandParameters> _moveProcessor = new CommandsProcessor<MoveCommandParameters>();
    [SerializeField] private CommandsProcessor<WeaponCommandParameters> _weaponProcessor = new CommandsProcessor<WeaponCommandParameters>();

    private CreateMovementCommand _createMovementCommand;

    private void Awake()
    {
        _createMovementCommand = new CreateMovementCommand();
        _createMovementCommand.Initialize();

        _moveProcessor.Initialize(transform);
        // Здесь была другая тестовая версия
        //_moveProcessor.InitializeNewWay(transform);
		_weaponProcessor.Initialize(transform);

    }

    private void Update()
    {
		_moveProcessor.TickCommandThreads();
		_weaponProcessor.TickCommandThreads();
    }
}
