using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats = null;
    [SerializeField] private CommandsProcessor<MoveCommandScriptableObject> _moveProcessor = new CommandsProcessor<MoveCommandScriptableObject>();
    [SerializeField] private CommandsProcessor<WeaponCommandParameters> _weaponProcessor = new CommandsProcessor<WeaponCommandParameters>();

    private void Start()
    {
        _moveProcessor.Initialize(transform);
        _weaponProcessor.Initialize(transform);
        // Increment enemies counter
        Game_Temp.Instance.EnemiesCounter.IncrementEnemies();
    }

    private void Update()
    {
        _moveProcessor.TickCommandThreads();
        _weaponProcessor.TickCommandThreads();
    }

    public void TakeDamage(float damage)
    {
        if (_stats.TakeDamage(damage))
        {
            // Add amount of points _stats.PointsForKill;
            // Die
        }
    }

    private void OnDestroy()
    {
        // Decrement enemies counter
        Game_Temp.Instance.EnemiesCounter.DectrementEnemies();
    }

    // Тут еще должен быть коллайдер или триггер, но он может быть и на пулях, хз
    // Возможно, что пули будут делать только урон попаданиями, а этот будет делать урон при контакте

    private void OnCollisionEnter2D(Collision2D collision) // Maybe OnCollisionStay even
    {
        //check for player

        Destroy(gameObject);
    }
}
