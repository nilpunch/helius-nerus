using UnityEngine;

public class StraightMove : BasicEnemyBullet
{
    private void Update()
    {
        if (_paused)
            return;
        _transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }
}