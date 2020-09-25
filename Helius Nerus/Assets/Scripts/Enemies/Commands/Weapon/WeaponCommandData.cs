using System;
using UnityEngine;

[System.Serializable]
public class WeaponCommandData
{
	[Tooltip("Префаб выпускаемой пули")]
	[SerializeField] private GameObject _bulletPrefab = null;
	[Tooltip("Задержка перед выстрелом")]
	[SerializeField] private float _delayBeforeShoot = 0.0f;
	[Tooltip("Задержка после выстрела")]
	[SerializeField] private float _delayAfterShoot = 0.0f;
	[Tooltip("Оффсет относительно позиции врага")]
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

	public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }
	public float DelayBeforeShoot { get => _delayBeforeShoot; set => _delayBeforeShoot = value; }
	public float DelayAfterShoot { get => _delayAfterShoot; set => _delayAfterShoot = value; }
	public Vector2 Position { get => _position; set => _position = value; }
	public Vector2 Direction { get => _direction; set => _direction = value; }
	public int BulletAmount { get => _bulletAmount; set => _bulletAmount = value; }
	public float SpreadAngle { get => _spreadAngleDegrees; set => _spreadAngleDegrees = value; }
	public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
	public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
	public float BulletSize { get => _bulletSize; set => _bulletSize = value; }
	public bool WorkOnce { get => _workOnce; set => _workOnce = value; }
}