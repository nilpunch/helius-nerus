using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponPrefs", menuName = "ScriptableObjects/Player weapons", order = 4)]
public class PlayerWeaponsParametrs : ScriptableObject
{
	public const float DEFAULT_RELOAD_TIME_CAP = 0.1f;

    [Tooltip("Позиция точки выстрела или оффсет")]
    [SerializeField] private Vector2 _position = Vector2.zero;
    [Tooltip("Напрваление выстрела")]
    [SerializeField] private Vector2 _direction = Vector2.up;
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

	/*  Не поянл зачем этот чанк кода, закомментил и всё работает :\  */
    //public PlayerWeaponsParametrs(float reloadTime, int bulletAmount, float spreadAngle, float bulletSize, float bulletDamage, float bulletSpeed)
    //{
    //    _position = Vector2.zero;
    //    _direction = Vector2.up;
    //    _reloadTime = reloadTime;
    //    _reloadTimeCap = DEFAULT_RELOAD_TIME_CAP;
    //    _bulletAmount = bulletAmount;
    //    _spreadAngleDegrees = spreadAngle;
    //    _bulletDamage = bulletDamage;
    //    _bulletSize = bulletSize;
    //    _bulletSpeed = bulletSpeed;
    //}

    public PlayerWeaponsParametrs Clone()
    {
        return (PlayerWeaponsParametrs)this.MemberwiseClone();
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }
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
    }
}
