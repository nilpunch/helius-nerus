using System;
using UnityEngine;

[System.Serializable]
public class WeaponCommandData
{
    [Tooltip("Энам с типом выпускаемой пули")]
    [SerializeField] private BulletTypes _bulletType = BulletTypes.StraightMove;
	[Tooltip("Задержка перед выстрелом")]
	[SerializeField] private float _delayBeforeShoot = 0.0f;
	[Tooltip("Задержка после выстрела")]
	[SerializeField] private float _delayAfterShoot = 0.0f;
	[Tooltip("Множитель времени для команды")]
	[SerializeField] private float _timeScale = 1.0f;
	[Tooltip("Позиция точки выстрела или оффсет")]
	[SerializeField] private Vector2 _position = Vector2.zero;
	[Tooltip("Напрваление выстрела")]
	[SerializeField] private Vector2 _direction = Vector2.up;
	[Tooltip("Количество выпускаемых за раз пуль")]
	[SerializeField] private int _bulletAmount = 1;
	[Tooltip("Конус выстрела в градусах")]
	[SerializeField] private float _spreadAngleDegrees = 45f;
	[Tooltip("Скорость полёта выпускаемой пули")]
	[SerializeField] private float _bulletSpeed = 1f;
	[Tooltip("Урон от попадания пули")]
	[SerializeField] private int _bulletDamage = 1;
	[Tooltip("Размер пули")]
	[SerializeField] private float _bulletSize = 1f;
	[Tooltip("Эта команда выполнится один раз?")]
	[SerializeField] private bool _workOnce = false;

	private WeaponCommandData _originalData;

	public void StoreData()
	{
		_originalData = (WeaponCommandData)this.MemberwiseClone();
	}

    public WeaponCommandData Clone()
    {
        return (WeaponCommandData)this.MemberwiseClone();
    }

	public void RestoreData()
	{
        _bulletType = _originalData._bulletType;
		_delayBeforeShoot = _originalData._delayBeforeShoot;
		_delayAfterShoot = _originalData._delayAfterShoot;
		_timeScale = _originalData._timeScale;
		_position = _originalData._position;
		_direction = _originalData._direction;
		_bulletAmount = _originalData._bulletAmount;
		_spreadAngleDegrees = _originalData._spreadAngleDegrees;
		_bulletSpeed = _originalData._bulletSpeed;
		_bulletDamage = _originalData._bulletDamage;
		_bulletSize = _originalData._bulletSize;
		_workOnce = _originalData._workOnce;
	}

	public BulletTypes BulletType { get => _bulletType; set => _bulletType = value; }
	public float DelayBeforeShoot { get => _delayBeforeShoot; set => _delayBeforeShoot = value; }
	public float DelayAfterShoot { get => _delayAfterShoot; set => _delayAfterShoot = value; }
	public float TimeScale { get => _timeScale; set => _timeScale = value; }
	public Vector2 Position { get => _position; set => _position = value; }
	public Vector2 Direction { get => _direction; set => _direction = value; }
	public int BulletAmount { get => _bulletAmount; set => _bulletAmount = value; }
	public float SpreadAngle { get => _spreadAngleDegrees; set => _spreadAngleDegrees = value; }
	public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
	public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
	public float BulletSize { get => _bulletSize; set => _bulletSize = value; }
	public bool WorkOnce { get => _workOnce; set => _workOnce = value; }
}