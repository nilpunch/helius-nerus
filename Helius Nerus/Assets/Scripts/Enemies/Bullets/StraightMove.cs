using UnityEngine;

public class StraightMove : MonoBehaviour, IBulletMovement
{
    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }
    private float _speedMultiplier = 1.0f;
    private Transform _transform = null;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }
}