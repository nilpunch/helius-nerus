using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[Tooltip("СО свойств пушки стартовых")]
    [SerializeField] private PlayerWeaponsParametrs _parametersSO = null;
	[SerializeField] private ModifierType[] _startModifiers = null;
    [SerializeField] private Player _player = null;

	private List<PlayerWeaponModifier> _modifiers = new List<PlayerWeaponModifier>();
	private PlayerWeaponsParametrs _parameters;
    private Transform _transform;
	private float _reloadTime = 0.0f;

	public PlayerWeaponsParametrs WeaponParameters
	{
		get => _parameters;
	}
    public List<PlayerWeaponModifier> WeaponModifiers
    {
        get => _modifiers;
    }

	public bool IsNoSooting
	{
		get => _player.IsNotShooting;
	}

	private void Awake()
	{
        if (_player == null)
            _player = Player.Instance;

		_parameters = _parametersSO.Clone();
		_transform = transform;

		foreach (ModifierType modifierType in _startModifiers)
		{
            //IPlayerWeaponModifier weaponModifier = ModifiersCollection.GetModifierByEnum(modifierType);
            PlayerWeaponModifier weaponModifier = WeaponModifierContainer.Instance.GetArtifact(modifierType);
			weaponModifier.OnPick(this);
			_modifiers.Add(weaponModifier);
		}
	}

    private void Update()
    {
        if (_player.IsNotShooting)
            return;

        _reloadTime -= TimeManager.PlayerDeltaTime;
		if (_reloadTime <= 0f )
        {
            _reloadTime = (1f / _parameters.BPS) + _reloadTime;
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

    public void AddModifier(PlayerWeaponModifier modifier)
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

    public void LoadFromSavedData(string parameters, string modifiers)
    {
        _parameters = PlayerWeaponsParametrs.DeserizliseFromString(parameters);

        for (int i = 0; i < _modifiers.Count; ++i)
        {
            _modifiers[i].OnDrop(this);
        }
        _modifiers.Clear();

        foreach (string s in modifiers.Split(','))
        {
#if UNITY_EDITOR
            Debug.Log("Weapon mod - " + s);
#endif

            ModifierType modifierType = (ModifierType)System.Enum.Parse(typeof(ModifierType), s);

            PlayerWeaponModifier modifier = WeaponModifierContainer.Instance.GetArtifact(modifierType);
            WeaponModifierContainer.Instance.RemoveArtifactFromPoolIfExists(modifierType);
            modifier.OnPick(this);
            _modifiers.Add(modifier);
        }
    }
}

public static class GetWeaponDescription
{
    public static string GetDescription(PlayerWeapon weapon)
    {
        StringBuilder sb = new StringBuilder();

        foreach (PlayerWeaponModifier modifier in weapon.WeaponModifiers)
        {
            sb.Append(LocalizationManager.Instance.GetLocalizedValue(
                WeaponModifierContainer.Instance.GetValueByKey(modifier.MyEnumValue).Description));
            sb.Append(System.Environment.NewLine);
        }
        return sb.ToString();
    }
}