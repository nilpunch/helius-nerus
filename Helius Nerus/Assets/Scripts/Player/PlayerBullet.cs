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

	private PlayerBulletParameters _bulletParameters = new PlayerBulletParameters();
	private Transform _transform = null;

	private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();

	private void Awake()
	{
		_transform = transform;
	}

	private void Update()
	{
		if (Pause.Paused)
			return;

		for (int i = 0; i < _modifiers.Count; ++i)
		{
			_modifiers[i].OnTick(this);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Нанести урон еще надо
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(BulletParameters.Damage);
			BulletParameters.Durability -= 1;
			for (int i = 0; i < _modifiers.Count; ++i)
			{
				_modifiers[i].OnHit(this, enemy);
			}
			// Какое-то условие по прочности
			if (BulletParameters.Durability <= 0)
				ReturnMeToPool();
		}
	}

	public void ReturnMeToPool()
	{
		// Temp
		BulletParameters.Durability = 1;
		// Temp

		for (int i = 0; i < _modifiers.Count; i++)
		{
			_modifiers[i].OnDestroy(this);
		}
		BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.PlayerBullet, gameObject);
	}

	public void SetModifiers(List<IPlayerWeaponModifier> modifiers)
	{
		_modifiers = modifiers;
	}
}
