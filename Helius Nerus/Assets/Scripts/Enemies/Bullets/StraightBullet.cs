using UnityEngine;

public class StraightBullet : MonoBehaviour
{
	[SerializeField] private float LiveTime = 1f;
	[SerializeField] private float Speed = 4f;

	private Transform _transform;

	private void Start()
	{
		_transform = gameObject.transform;
	}

	void Update()
    {
		LiveTime -= TimeManager.EnemyDeltaTime;
		if (LiveTime <= 0f)
		{
			Destroy(gameObject);
		}

		_transform.position += _transform.up * Speed * TimeManager.EnemyDeltaTime;
    }
}
