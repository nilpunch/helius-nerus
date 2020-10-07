using UnityEngine;

public class StraightMove : BasicEnemyBullet
{
    private void Update()
    {
        _transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }
}