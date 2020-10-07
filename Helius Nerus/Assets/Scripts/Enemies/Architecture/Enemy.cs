using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats = null;
    [SerializeField] private CommandsProcessor<MoveCommandScriptableObject> _moveProcessor = new CommandsProcessor<MoveCommandScriptableObject>();
    [SerializeField] private CommandsProcessor<WeaponCommandScriptableObject> _weaponProcessor = new CommandsProcessor<WeaponCommandScriptableObject>();
    [SerializeField] private EnemyTypes _type = EnemyTypes.SquareEnemy;

    private void Awake()
    {
        _stats.Reset();
    }

    private void Update()
    {
        _moveProcessor.TickCommandThreads();
        _weaponProcessor.TickCommandThreads();
    }

    public void TakeDamage(float damage)
    {
        if (_stats.TakeDamage(damage) == true)
        {
            Die();
        }
    }

    private void Die()
    {
        Game_Temp.Instance.EnemiesCounter.DectrementEnemies();
        EnemyPoolContainer.Instance.ReturnObjectToPool(_type, gameObject);
    }

    public void Reset()
    {
        _moveProcessor.Initialize(transform);
        _weaponProcessor.Initialize(transform);
        _stats.Reset();
        Game_Temp.Instance.EnemiesCounter.IncrementEnemies();
    }


    // Тут еще должен быть коллайдер или триггер, но он может быть и на пулях, хз
    // Возможно, что пули будут делать только урон попаданиями, а этот будет делать урон при контакте

    private void OnCollisionEnter2D(Collision2D collision) // Maybe OnCollisionStay even
    {
        //check for player
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(_stats.DamageOnContact);
        }
        else if (collision.gameObject.layer == 12) // Screen end collider
        {
            Die();
        }
    }
}
