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
    }

    [Tooltip("Количество ХП врага")]
    [SerializeField] private float _Health = 1.0f;
    [Tooltip("Количество очков за убийство врага")]
    [SerializeField] private int _pointsForKill = 10;
    [Tooltip("Наносимый урон при контакте")]
    [SerializeField] private int _damageOnContact = 1;

    public bool TakeDamage(float damage)
    {
        _Health -= damage;
        if (_Health <= 0.0f)
            return true;
        return false;
    }
}