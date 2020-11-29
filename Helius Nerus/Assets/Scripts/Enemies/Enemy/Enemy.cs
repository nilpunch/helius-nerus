using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour, IReturnableToPool, IDealDamageToPlayer, ITakeDamageFromPlayer
{
	public static event System.Action<int> EnemyDie = delegate { };

	[SerializeField] private EnemyStats _stats = null;
	[SerializeField] private EnemyTypes _type = EnemyTypes.SquareEnemy;
	[SerializeField] private BulletTypes _bulletType = BulletTypes.AngelBullet;

	private bool _isDead = false;

	public BulletTypes BulletType => _bulletType;

	public EnemyStats EnemyStats => _stats;

	public int Damage
	{
		get => _stats.DamageOnContact;
		set => _stats.DamageOnContact = value;
	}

	private void Awake()
	{
		_stats.Reset();
	}

	private void OnEnable()
	{
		Player.PlayerDie += Player_PlayerDie;
		Enabled();
	}

	public virtual void Enabled() { }

	private void OnDisable()
	{
		Player.PlayerDie -= Player_PlayerDie;
		Disabled();
	}

	public virtual void Disabled() { }

	private void Player_PlayerDie()
	{
		ReturnMeToPool();
	}

	private void Update()
	{
		OnUpdate();
	}

	public virtual void OnUpdate() { }

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
		EnemyDie.Invoke(_stats.PointsForKill);

		_isDead = true;
		float drop = Random.Range(0.0f, 1.0f);
		if (drop <= _stats.DropChance)
		{
			UpgrageCollection.Instance.GetRandomUpgrade().transform.position = transform.position;
		}
		ReturnMeToPool();
	}

	public virtual void OnReset() { }

	public void Reset()
	{
		OnReset();
		_stats.Reset();
		_isDead = false;
		Level.EnemiesCounter.IncrementEnemies();
	}

	public void ReturnMeToPool()
	{
		EnemyPoolContainer.Instance.ReturnObjectToPool(_type, gameObject);
		Level.EnemiesCounter.DectrementEnemies();
	}

	public int GetMyDamage()
	{
		return Damage;
	}
}
