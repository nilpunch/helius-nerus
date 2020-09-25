using UnityEngine;

public class WeaponCommandData
{
	[SerializeField] GameObject _bulletPrefab = null;
	[SerializeField] private Vector2 _position = Vector2.zero;
	[SerializeField] private Vector2 _direction = Vector2.up;
	[SerializeField] private int _bulletAmount = 1;
	[SerializeField] private float _spreadAngle = 45f;
	[SerializeField] private float _bulletSpeed = 1f;
	[SerializeField] private int _bulletDamage = 1;
	[SerializeField] private float _bulletSize = 1f;

	public GameObject BulletPrefab => _bulletPrefab;
	public Vector2 Position => _position;
	public Vector2 Direction => _direction;
	public int BulletAmount => _bulletAmount;
	public float SpreadAngle => _spreadAngle;
	public float BulletSpeed => _bulletSpeed;
	public int BulletDamage => _bulletDamage;
	public float BulletSize => _bulletSize;
}