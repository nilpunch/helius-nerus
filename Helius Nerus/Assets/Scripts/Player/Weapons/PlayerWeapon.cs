using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	public PlayerWeaponsParametrs WeaponParameters
	{
		get => _parameters;
	}

	public bool IsNoSooting
	{
		get => _player.IsNoShooting;
	}

	[Tooltip("СО свойств пушки стартовых")]
    [SerializeField] private PlayerWeaponsParametrs _parametersSO = null;
	[SerializeField] private ModifierType[] _startModifiers = null;
    [SerializeField] private Player _player = null;

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
			weaponModifier.OnPick(this);
			_modifiers.Add(weaponModifier);
		}
	}

    private void Update()
    {
        if (Pause.Paused || _player.IsNoShooting)
            return;

        _reloadTime += Time.deltaTime;
        if (_reloadTime >= 1f / _parameters.BPS)
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
			PlayerBullet pBullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet).GetComponent<PlayerBullet>();
  
            pBullet.SetModifiers(_modifiers);

            pBullet.BulletParameters.SpeedMultiplier = _parameters.BulletSpeed;
            pBullet.BulletParameters.Damage = _parameters.BulletDamage * _parameters.DamageMult;
			pBullet.Transform.position = _transform.position;
			pBullet.Transform.position += (Vector3)_parameters.Position;
			pBullet.Transform.localScale = Vector3.one * _parameters.BulletSize;
			pBullet.Transform.localEulerAngles = new Vector3(0f, 0f, _parameters.WeaponAngle + (_angleStep * (i - _halfBulletAmount)));

			pBullet.OnBulletEnable();
        }

		for (int i = 0; i < _modifiers.Count; ++i)
		{
			_modifiers[i].OnWeaponShoot(this);
		}
    }

    public void AddModifier(IPlayerWeaponModifier modifier)
    {
		modifier.OnPick(this);
		_modifiers.Add(modifier);
    }

    public void SetParametrs(PlayerWeaponsParametrs parameters)
    {
		_parameters = parameters.Clone();
    }

    public void ApplyPair(ArtifactUpgradePair pair)
    {
        _parameters.ApplyModifier(pair.WeaponsParametrs);
        _modifiers.Add(pair.WeaponModifier);
    }
}