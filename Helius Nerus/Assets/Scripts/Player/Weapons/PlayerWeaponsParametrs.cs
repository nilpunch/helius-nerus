using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponPrefs", menuName = "ScriptableObjects/Player weapons", order = 4)]
public class PlayerWeaponsParametrs : ScriptableObject
{
    [Tooltip("Позиция точки выстрела или оффсет")]
    [SerializeField] private Vector2 _position = Vector2.zero;
    [Tooltip("Напрваление выстрела")]
    [SerializeField] private Vector2 _direction = Vector2.up;
    [Tooltip("Время перезарядки пули")]
    [SerializeField] private float _reloadTime = 1f;
    [Tooltip("Количество выпускаемых за раз пуль")]
    [SerializeField] private int _bulletAmount = 1;
    [Tooltip("Конус выстрела в градусах")]
    [SerializeField] private float _spreadAngleDegrees = 45f;
    [Tooltip("Скорость полёта выпускаемой пули")]
    [SerializeField] private float _bulletSpeed = 1f;
    [Tooltip("Урон от попадания пули")]
    [SerializeField] private float _bulletDamage = 1;
    [Tooltip("Размер пули")]
    [SerializeField] private float _bulletSize = 1f;

    private BulletTypes _myType = BulletTypes.PlayerBullet;

    public PlayerWeaponsParametrs Clone()
    {
        return (PlayerWeaponsParametrs)this.MemberwiseClone();
    }

    public BulletTypes Type
    {
        get => _myType;
    }
    public Vector2 Position { get => _position; set => _position = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float ReloadTime { get => _reloadTime; set => _reloadTime = value; }
    public int BulletAmount { get => _bulletAmount; set => _bulletAmount = value; }
    public float SpreadAngle { get => _spreadAngleDegrees; set => _spreadAngleDegrees = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public float BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
    public float BulletSize { get => _bulletSize; set => _bulletSize = value; }
}
