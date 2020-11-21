using UnityEngine;

public class StraightMove : EnemyBullet
{
    private void Update()
    {
        _transform.Translate(Vector3.up * TimeManager.EnemyDeltaTime * _speedMultiplier, Space.Self);
    }
}