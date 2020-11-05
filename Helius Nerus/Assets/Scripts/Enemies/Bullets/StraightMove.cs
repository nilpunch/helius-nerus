using UnityEngine;

public class StraightMove : BasicEnemyBullet
{
    private void Update()
    {
        _transform.Translate(Vector3.up * TimeManager.EnemyDeltaTime * _speedMultiplier, Space.Self);
    }
}