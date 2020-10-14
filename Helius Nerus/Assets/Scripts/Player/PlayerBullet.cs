using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IReturnableToPool
{
    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }
    public int Durability
    {
        set => _durability = value;
    }
    public float Damage
    {
        set => _damage = value;
    }

    private float _speedMultiplier = 1.0f;
    private Transform _transform = null;
    private int _durability = 1;
    private float _damage = 1.0f;

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
		_transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Нанести урон еще надо
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            --_durability;
            for (int i = 0; i < _modifiers.Count; ++i)
            {
                _modifiers[i].OnHit(this);
            }
            // Какое-то условие по прочности
            if (_durability <= 0)
                ReturnMeToPool();
        }
    }

    public void ReturnMeToPool()
    {
        // Temp
        _durability = 1;
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
