using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponPrefs", menuName = "ScriptableObjects/Player weapons", order = 4)]
public class PlayerWeaponsParametrs : ScriptableObject
{
	public const float DEFAULT_RELOAD_TIME_CAP = 0.1f;

    [Tooltip("Позиция точки выстрела или оффсет")]
    [SerializeField] private Vector2 _position = Vector2.zero;
    [Tooltip("Напрваление выстрела")]
    [SerializeField] private float _weaponAngle = 0f;
    [Tooltip("Количество выпускаемых в секунду пуль")]
    [SerializeField] private float _bulletsPerSec = 1f;
    [Tooltip("Количество выпускаемых за раз пуль")]
    [SerializeField] private int _bulletAmount = 1;
    [Tooltip("Конус выстрела в градусах")]
    [SerializeField] private float _spreadAngleDegrees = 45f;
    [Tooltip("Скорость полёта выпускаемой пули")]
    [SerializeField] private float _bulletSpeed = 1f;
    [Tooltip("Урон от попадания пули")]
    [SerializeField] private float _bulletDamage = 1f;
	[Tooltip("Множитель урона")]
	[SerializeField] private float _damageMult = 1f;
	[Tooltip("Размер пули")]
    [SerializeField] private float _bulletSize = 1f;

    public PlayerWeaponsParametrs Clone()
    {
        return (PlayerWeaponsParametrs)this.MemberwiseClone();
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public float WeaponAngle { get => _weaponAngle; set => _weaponAngle = value; }
    public float BPS { get => _bulletsPerSec; set => _bulletsPerSec = value; }
    public int BulletAmount { get => _bulletAmount; set => _bulletAmount = value; }
    public float SpreadAngle { get => _spreadAngleDegrees; set => _spreadAngleDegrees = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public float BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
	public float DamageMult { get => _damageMult; set => _damageMult = value; }
    public float BulletSize { get => _bulletSize; set => _bulletSize = value; }

	public void ApplyModifier(PlayerWeaponsParametrs mod)
    {
		_bulletsPerSec += mod._bulletsPerSec;

        _bulletAmount += mod._bulletAmount;

        _spreadAngleDegrees += mod._spreadAngleDegrees;
        _bulletSpeed += mod._bulletSpeed;
        _bulletSize += mod._bulletSize;
        _bulletDamage += mod._bulletDamage;
        _damageMult += mod._damageMult;
    }
}
