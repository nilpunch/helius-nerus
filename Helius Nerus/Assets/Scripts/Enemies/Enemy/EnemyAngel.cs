using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngel : Enemy
{
	[SerializeField] Transform _transform = null;
	[SerializeField] float _homingFactor = 20f;
	[SerializeField] float _speed = 1f;
	[SerializeField] float _reloadTime = 1f;
	[SerializeField] float _bulletSpeedMultiplyer = 1f;

	private float _timeElapsed = 0f;

	public override void Enabled()
	{
		_timeElapsed = 0f;
	}

	public override void OnUpdate()
	{
		if (Player.Instance.gameObject.activeSelf)
		{
			Vector3 direction = _transform.position - Player.Instance.transform.position;
			direction.Normalize();
			float rotation = Vector3.Cross(direction, _transform.up).z;

			float homingCoefficient = Mathf.Abs(rotation) > 0.01f ? _homingFactor : 0f;

			_transform.localEulerAngles += new Vector3(0f, 0f,
				Mathf.Sign(rotation) * homingCoefficient * TimeManager.PlayerDeltaTime);
		}

		_transform.Translate(Vector3.up * _speed * TimeManager.EnemyDeltaTime, Space.Self);

		_timeElapsed += TimeManager.EnemyDeltaTime;

		if (_timeElapsed >= _reloadTime)
		{
			_timeElapsed = 0f;
			GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletType);
			(bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = _bulletSpeedMultiplyer;
			bullet.transform.position = _transform.position;
			bullet.transform.localScale = 1f * Vector3.one;
			bullet.transform.localEulerAngles = _transform.localEulerAngles;
		}
	}

	public override void OnReset()
	{

	}
}
