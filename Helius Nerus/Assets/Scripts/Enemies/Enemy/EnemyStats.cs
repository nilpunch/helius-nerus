using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    [Tooltip("Полное количество ХП врага")]
    [SerializeField] private float _FullHealth = 1.0f;
    [Tooltip("Количество очков за убийство врага")]
    [SerializeField] private int _pointsForKill = 10;
    [Tooltip("Наносимый урон при контакте")]
    [SerializeField] private int _damageOnContact = 1;
    [Tooltip("Вероятность выпадения апгрейда")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _dropChance = 0.02f;

	private float _health;

    public int PointsForKill
    {
        get => _pointsForKill;
    }
    public int DamageOnContact
    {
        get => _damageOnContact;
        set => _damageOnContact = value;
    }
    public float DropChance
    {
        get => _dropChance;
    }

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