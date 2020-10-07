using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public int PointsForKill
    {
        get => _pointsForKill;
    }
    public int DamageOnContact
    {
        get => _damageOnContact;
        set => _damageOnContact = value;
    }

    [Tooltip("Полное количество ХП врага")]
    [SerializeField] private float _FullHealth = 1.0f;
    [Tooltip("Количество очков за убийство врага")]
    [SerializeField] private int _pointsForKill = 10;
    [Tooltip("Наносимый урон при контакте")]
    [SerializeField] private int _damageOnContact = 1;

    private float _health;

    public bool TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0.0f)
            return true;
        return false;
    }

    public void Reset()
    {
        _health = _FullHealth;
    }
}