using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IReturnableToPool
{
	public PlayerBulletParameters BulletParameters
	{
		get => _bulletParameters;
	}
	public Transform Transform
	{
		get => _transform;
	}
	public Dictionary<ModifierType, int> ModifiersProcCount
	{
		get => _procedModifiers;
	}
	public List<IPlayerWeaponModifier> Modifiers
	{
		get => _modifiers;
	}
	public bool CollideWithEnemies { get; set; } = true;

	private PlayerBulletParameters _bulletParameters = new PlayerBulletParameters();
	private Transform _transform = null;

	private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();
	private Dictionary<ModifierType, int> _procedModifiers = new Dictionary<ModifierType, int>();

	private void Awake()
	{
		_transform = transform;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Нанести урон еще надо
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		if (enemy != null && CollideWithEnemies)
		{
			enemy.TakeDamage(BulletParameters.Damage);
			BulletParameters.Durability -= 1;
			for (int i = 0; i < _modifiers.Count; ++i)
			{
				_modifiers[i].OnHit(this, enemy);
			}
			// Какое-то условие по прочности
			if (BulletParameters.Durability <= 0)
			{
				for (int i = 0; i < _modifiers.Count; i++)
				{
					_modifiers[i].OnBulletDestroy(this);
				}
				ReturnMeToPool();
			}
		}
	}

	public void ReturnMeToPool()
	{
		BulletParameters.Durability = 1;
		CollideWithEnemies = false;
		_procedModifiers.Clear();
		BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.PlayerBullet, gameObject);
	}

	public void SetModifiers(List<IPlayerWeaponModifier> modifiers)
	{
		_modifiers = modifiers;
	}

	public void OnBulletEnable()
	{
		for (int i = 0; i < _modifiers.Count; ++i)
		{
			_modifiers[i].OnBulletEnable(this);
		}
		CollideWithEnemies = true;
	}
}
