using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IReturnableToPool
{
	private PlayerBulletParameters _bulletParameters = new PlayerBulletParameters();
	private Transform _transform = null;

	private List<PlayerWeaponModifier> _modifiers = new List<PlayerWeaponModifier>();

	public PlayerBulletParameters BulletParameters
	{
		get => _bulletParameters;
	}
	public Transform Transform
	{
		get => _transform;
	}
	public List<PlayerWeaponModifier> Modifiers
	{
		get => _modifiers;
	}
	public bool CollideWithEnemies { get; set; } = true;

	private void Awake()
	{
		_transform = transform;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Нанести урон еще надо
		ITakeDamageFromPlayer enemy = (collision.gameObject.GetComponent(typeof(ITakeDamageFromPlayer)) as ITakeDamageFromPlayer);
		if (enemy != null && CollideWithEnemies)
		{
			enemy.TakeDamage(BulletParameters.Damage);
			BulletParameters.Durability -= 1;
			for (int i = 0; i < _modifiers.Count; ++i)
			{
				_modifiers[i].OnHit(this, collision.gameObject);
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
		BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.PlayerBullet, gameObject);
	}

	public void SetModifiers(List<PlayerWeaponModifier> modifiers)
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
