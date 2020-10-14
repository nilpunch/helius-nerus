using UnityEngine;

public class Enemy : MonoBehaviour, IReturnableToPool, IDealDamageToPlayer
{
    [SerializeField] private EnemyStats _stats = null;
    [SerializeField] private CommandsProcessor<MoveCommandScriptableObject> _moveProcessor = new CommandsProcessor<MoveCommandScriptableObject>();
    [SerializeField] private CommandsProcessor<WeaponCommandScriptableObject> _weaponProcessor = new CommandsProcessor<WeaponCommandScriptableObject>();
    [SerializeField] private EnemyTypes _type = EnemyTypes.SquareEnemy;

    private bool _isDead = false;

    public int Damage
    {
        get => _stats.DamageOnContact;
        set => _stats.DamageOnContact = value;
    }

    private void Awake()
    {
        _stats.Reset();
    }

    private void Update()
    {
        if (Pause.Paused)
            return;

        _moveProcessor.TickCommandThreads();
        _weaponProcessor.TickCommandThreads();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
            return;

        if (_stats.TakeDamage(damage) == true)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        float drop = UnityEngine.Random.Range(0.0f, 1.0f);
        if (drop <= _stats.DropChance)
        {
            UpgrageCollection.Instance.GetRandomUpgrade().transform.position = transform.position;
        }
        ReturnMeToPool();
    }

    public void Reset()
    {
        _moveProcessor.Initialize(transform);
        _weaponProcessor.Initialize(transform);
        _stats.Reset();
        _isDead = false;
        Level.Instance.EnemiesCounter.IncrementEnemies();
    }

    public void ReturnMeToPool()
    {
        Level.Instance.EnemiesCounter.DectrementEnemies();
        EnemyPoolContainer.Instance.ReturnObjectToPool(_type, gameObject);
    }

    public int GetMyDamage()
    {
        return Damage;
    }
}
