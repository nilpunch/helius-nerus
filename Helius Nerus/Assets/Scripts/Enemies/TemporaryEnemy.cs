using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    [SerializeField] private CommandsProcessor<MoveCommandParameters> _moveProcessor = new CommandsProcessor<MoveCommandParameters>();
    [SerializeField] private CommandsProcessor<WeaponCommandParameters> _weaponProcessor = new CommandsProcessor<WeaponCommandParameters>();

    private void Awake()
    {
		_moveProcessor.Initialize(transform);
		_weaponProcessor.Initialize(transform);
    }

    private void Update()
    {
		_moveProcessor.Tick();
		_weaponProcessor.Tick();
    }
}
