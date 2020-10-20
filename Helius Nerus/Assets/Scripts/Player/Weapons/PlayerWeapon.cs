using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	public PlayerWeaponsParametrs WeaponParameters
	{
		get => _parameters;
	}

	[Tooltip("СО свойств пушки стартовых")]
    [SerializeField] private PlayerWeaponsParametrs _parametersSO = null;
	[SerializeField] private ModifierType[] _startModifiers = null;

	private List<IPlayerWeaponModifier> _modifiers = new List<IPlayerWeaponModifier>();
	private PlayerWeaponsParametrs _parameters;
    private Transform _transform;

	private float _reloadTime = 0.0f;

	private void Awake()
	{
		_parameters = _parametersSO.Clone();
		_transform = transform;

		foreach (ModifierType modifierType in _startModifiers)
		{
			IPlayerWeaponModifier weaponModifier = ModifiersCollection.GetModifierByEnum(modifierType);
			_modifiers.Add(weaponModifier);
		}
	}

    private void Update()
    {
        if (Pause.Paused)
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
            GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet);

            PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
  
            pBullet.SetModifiers(_modifiers);

            pBullet.BulletParameters.SpeedMultiplier = _parameters.BulletSpeed;
            pBullet.BulletParameters.Damage = _parameters.BulletDamage;
            bullet.transform.position = _transform.position;
            bullet.transform.position += (Vector3)_parameters.Position;
            bullet.transform.localScale = Vector3.one * _parameters.BulletSize;
            bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, _parameters.Direction) + (_angleStep * (i - _halfBulletAmount)));

			pBullet.OnShoot();
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