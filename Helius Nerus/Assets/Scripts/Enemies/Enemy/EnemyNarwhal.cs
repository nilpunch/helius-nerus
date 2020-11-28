﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EnemyNarwhal : Enemy
{
	[SerializeField] Transform _transform = null;
	[SerializeField] float _homingFactor = 20f;
	[SerializeField] float _burstDistance = 1f;
	[SerializeField] float _burstDuration = 0.5f;

	//private float _timeElapsed = 0f;
	private bool _inBurst = false;

	public override void Enabled()
	{
		_inBurst = false;
	}

	public override void OnUpdate()
	{
		if (Player.Instance.gameObject.activeSelf && _inBurst == false)
		{
			Vector3 direction = _transform.position - Player.Instance.transform.position;
			direction.Normalize();
			float rotation = Vector3.Cross(direction, _transform.up).z;

			float homingCoefficient = 0f;
			if (Mathf.Abs(rotation) > 0.01f)
			{
				homingCoefficient = _homingFactor;
			}
			else
			{
				homingCoefficient = 0f;
				_inBurst = true;
				_transform.DOMove(_transform.position + _transform.up * _burstDistance, _burstDuration)
						  .SetEase<Tween>(Ease.OutExpo)
						  .OnComplete<Tween>(() => _inBurst = false);
				return;
			}

			_transform.localEulerAngles += new Vector3(0f, 0f,
				Mathf.Sign(rotation) * homingCoefficient * TimeManager.PlayerDeltaTime);
		}

		

		//_transform.Translate(Vector3.up * _speed * TimeManager.EnemyDeltaTime, Space.Self);

		//_timeElapsed += TimeManager.EnemyDeltaTime;

		//if (_timeElapsed >= _reloadTime)
		//{
		//	_timeElapsed = 0f;
		//	GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletType);
		//	(bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = _bulletSpeedMultiplyer;
		//	bullet.transform.position = _transform.position;
		//	bullet.transform.localScale = 1f * Vector3.one;
		//	bullet.transform.localEulerAngles = _transform.localEulerAngles;
		//}
	}
}