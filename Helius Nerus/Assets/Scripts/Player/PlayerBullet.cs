using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IReturnableToPool
{
    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }
    private float _speedMultiplier = 1.0f;
    private Transform _transform = null;

    private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Нанести урон еще надо
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GetComponent<DealContactDamage>().Damage);
            for (int i = 0; i < _modifiers.Count; ++i)
            {
                _modifiers[i].OnHit();
            }
            // Какое-то условие по прочности
            if (true)
                ReturnMeToPool();
        }
    }

    public void ReturnMeToPool()
    {
        // Temp
        ClearModifiers();
        // Temp

        for (int i = 0; i < _modifiers.Count; i++)
        {
            IPlayerWeaponModifier modifier = (IPlayerWeaponModifier)_modifiers[i];
            _modifiers[i].OnDestroy();
        }
        BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.PlayerBullet, gameObject);
    }

    public void AddMofifier(IPlayerWeaponModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public void ClearModifiers()
    {
        _modifiers.Clear();
    }

    public void SetModifiers(List<IPlayerWeaponModifier> modifiers)
    {
        _modifiers = modifiers;
    }
}
