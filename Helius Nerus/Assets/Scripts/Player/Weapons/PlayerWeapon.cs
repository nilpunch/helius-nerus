using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Tooltip("СО свойств пушки стартовых")]
    [SerializeField] private PlayerWeaponsParametrs _parameters;

    private float _reloadTime = 0.0f;

    private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();

    private Transform _transform;

    private bool _paused = false;

    private void Awake()
    {
        _transform = transform;

        Pause.GamePaused += Pause_GamePaused;
        Pause.GameUnpaused += Pause_GameUnpaused;
    }

    private void OnDestroy()
    {
        Pause.GamePaused -= Pause_GamePaused;
        Pause.GameUnpaused -= Pause_GameUnpaused;
    }

    private void Pause_GameUnpaused()
    {
        _paused = false;
    }

    private void Pause_GamePaused()
    {
        _paused = true;
    }

    private void Update()
    {
        if (_paused)
            return;
        _reloadTime += Time.deltaTime;
        if (_reloadTime >= _parameters.ReloadTime)
        {
            _reloadTime = 0.0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        float _halfBulletAmount = 0f;
        float _angleStep = 0f;

        if (_parameters.BulletAmount > 1)
        {
            _halfBulletAmount = (_parameters.BulletAmount - 1) / 2f;
            _angleStep = _parameters.SpreadAngle / _halfBulletAmount / 2f;
        }
        else
        {
            _halfBulletAmount = 0f;
            _angleStep = 0f;
        }

        for (int i = 0; i < _parameters.BulletAmount; ++i)
        {
            GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(_parameters.Type);

            PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
            // Temp
            //if (_modifiers != null)
                pBullet.SetModifiers(_modifiers);
            // Temp

            pBullet.SpeedMultiplier = _parameters.BulletSpeed;
            pBullet.Damage = _parameters.BulletDamage;
            bullet.transform.position = _transform.position;
            bullet.transform.position += (Vector3)_parameters.Position;
            bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, _parameters.Direction) + (_angleStep * (i - _halfBulletAmount)));
        }
    }

    public void AddModifier(IPlayerWeaponModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public void SetParametrs(PlayerWeaponsParametrs parameters)
    {
        _parameters = parameters.Clone();
    }
}