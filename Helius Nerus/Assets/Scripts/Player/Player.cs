﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	public static Player Instance
    {
        get;
        private set;
    }

    public static event System.Action PlayerHelathChanged = delegate { };
    public static event System.Action PlayerTookDamage = delegate { };
    public static event System.Action PlayerBeforeDie = delegate { };
    public static event System.Action PlayerDie = delegate { };
    public static event System.Action PlayerResurrection = delegate { };

    public static PlayerParameters PlayerParameters
    {
        get => Instance._playerParameters;
    }
	public static Rigidbody2D Rigidbody2D
	{
		get => Instance._rigidbody2D;
	}
    public static SpriteRenderer SpriteRenderer
    {
        get => Instance._renderer;
    }
	public static PlayerWeapon[] PlayerWeapons
	{
		get => Instance._weapons;
	}

	[SerializeField] private PlayerMovement _playerMovement = null;
	[Space]
	[SerializeField] private PlayerParameters _playerParametersSO = null;
    [SerializeField] private PlayerWeapon[] _weapons = null;
	[SerializeField] private ArtifactType[] _startArtifacts = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private SpriteRenderer _renderer = null;
	
	private List<IPlayerArtifact> _artifacts = new List<IPlayerArtifact>();
    private PlayerParameters _playerParameters = null;

    private void Awake()
    {
		Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
		_playerMovement.Init();
		RestrartPlayer();
	}

	private void Update()
	{
		_playerMovement.Update();
	}

	private void RestrartPlayer()
    {
		_playerParameters = _playerParametersSO.Clone();
		_playerParameters.CurrentHealth = _playerParameters.MaxHealth;

		for (int i = 0; i < _artifacts.Count; ++i)
		{
			_artifacts[i].OnDrop();
		}

		_artifacts.Clear();

		foreach (ArtifactType artifactType in _startArtifacts)
		{
			IPlayerArtifact artifact = ArtifactsCollection.GetArtifactByEnum(artifactType);
			artifact.OnPick();
			_artifacts.Add(artifact);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDealDamageToPlayer dealDamageToPlayer = (collision.gameObject.GetComponent(typeof(IDealDamageToPlayer)) as IDealDamageToPlayer);
		if (dealDamageToPlayer != null)
		{
			TakeDamage(dealDamageToPlayer.GetMyDamage());
			return;
		}
		UpgradeBase upgrade = collision.GetComponent<UpgradeBase>();
		if (upgrade != null)
		{
			upgrade.UpgradeCharacter();
			return;
		}
	}

	public void TakeDamage(int damage)
	{
		if (_rigidbody2D.simulated == false)
			return;

		if (damage >= 0)
		{
			_playerParameters.CurrentHealth -= damage;
			PlayerHelathChanged.Invoke();
		}

		if (_playerParameters.CurrentHealth <= 0)
		{
			PlayerBeforeDie.Invoke();

			if (_playerParameters.CurrentHealth <= 0)
			{
				PlayerDie.Invoke();
				gameObject.SetActive(false);
			}
			else
			{
				PlayerTookDamage.Invoke();
				PlayerResurrection.Invoke();
			}
		}
		else
		{
			PlayerTookDamage.Invoke();
		}
	}

	public void IncrementHealth()
	{
		if (_playerParameters.CurrentHealth < _playerParameters.MaxHealth)
		{
			_playerParameters.CurrentHealth++;
			PlayerHelathChanged.Invoke();
		}
	}

    public void AddArtifact(ArtifactUpgradePair pair)
    {
        if (pair.IsShipMod)
        {
            IPlayerArtifact artifact = pair.Artifact.Clone();
            _artifacts.Add(artifact);
            artifact.OnPick();
        }
    }
}
