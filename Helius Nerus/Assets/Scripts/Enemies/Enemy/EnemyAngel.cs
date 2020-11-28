using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngel : Enemy
{
	[SerializeField] Transform _transform = null;
	[SerializeField] float _homingFactor = 20f;
	[SerializeField] float _speed = 1f;

	private Coroutine _coroutine = null;

	public override void Enabled()
	{
		
	}

	public override void Disabled()
	{
		if (_coroutine != null)
		{
			StopCoroutine(_coroutine);
		}
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
	}

	public override void OnReset()
	{

	}
}
