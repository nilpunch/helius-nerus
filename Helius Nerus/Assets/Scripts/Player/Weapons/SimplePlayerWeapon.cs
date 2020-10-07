using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerWeapon : MonoBehaviour
{
    [SerializeField] private float _reloadTime = 0.5f;
    [SerializeField] private float _damage = 1.0f;
    [SerializeField] private float _bulletSpeed = 5.0f;
    private float _reloading = 0.0f;
    private BulletTypes _bulletType = BulletTypes.PlayerBullet;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _reloading += Time.deltaTime;
        if (_reloading >= _reloadTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(_bulletType);
        _reloading = 0.0f;
        bullet.transform.position = _transform.position;
        bullet.transform.localEulerAngles = Vector3.up;
        bullet.GetComponent<PlayerBullet>().SpeedMultiplier = _bulletSpeed;
        bullet.GetComponent<DealContactDamage>().Damage = _damage;
    }
}
