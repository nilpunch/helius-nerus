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

	public GameObject BulletPrefab => _bulletPrefab;
	public float DelayBeforeShoot=> _delayBeforeShoot;
	public float DelayAfterShoot => _delayAfterShoot;
	public Vector2 Position => _position;
	public Vector2 Direction => _direction;
	public int BulletAmount => _bulletAmount;
	public float SpreadAngle => _spreadAngleDegrees;
	public float BulletSpeed => _bulletSpeed;
	public int BulletDamage => _bulletDamage;
	public float BulletSize => _bulletSize;
	public bool WorkOnce => _workOnce;
}