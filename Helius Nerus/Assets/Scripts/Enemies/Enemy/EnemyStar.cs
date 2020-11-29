using DG.Tweening;
using UnityEngine;

public class EnemyStar : Enemy
{
	[SerializeField] private Transform _transform = null;
	[SerializeField] private int _bulletsAmount = 8;
	[SerializeField] private float _bulletsSpeed = 1f;
	[SerializeField] private float _travelTime = 1f;
	[SerializeField] private float _travelDistance = 1f;

	private bool _isMoving = false;
	private bool _clockwise = true;

	public override void Enabled()
	{
		_transform.DOKill();
		_clockwise = Random.Range(0, 2) == 0 ? true : false;
	}

	public override void Disabled()
	{
		_transform.DOKill();
	}

	public override void OnUpdate()
	{
		if (_isMoving == false)
		{
			_isMoving = true;
			_transform.DOMove(_transform.position + Vector3.down * _travelDistance, _travelTime)
				  .SetEase<Tween>(Ease.InOutCubic)
				  .OnComplete(() => 
				  {
					  ShootInCircle();
					  _isMoving = false;
				  });
			float rotation = _clockwise ? Random.Range(90f, 180f) : Random.Range(-180f, -90);
			_clockwise = !_clockwise;
			_transform.DORotate(_transform.eulerAngles + new Vector3(0f, 0f, rotation), _travelTime)
					  .SetEase<Tween>(Ease.InOutCubic);
		}
	}

	private void ShootInCircle()
	{
		float _halfBulletAmount = 0f;
		float _angleStep = 0f;

		if (_bulletsAmount > 1)
		{
			_halfBulletAmount = (_bulletsAmount) / 2f;
			_angleStep = 360f / _halfBulletAmount / 2f;
		}
		else
		{
			_halfBulletAmount = 0f;
			_angleStep = 0f;
		}

		for (int i = 0; i < _bulletsAmount; ++i)
		{
			GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletType);
			(bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = _bulletsSpeed;
			bullet.transform.position = _transform.position;
			bullet.transform.localScale = 1 * Vector3.one;
			bullet.transform.localEulerAngles = new Vector3(0f, 0f, _transform.eulerAngles.z + (_angleStep * (i - _halfBulletAmount)));
		}
	}
}
