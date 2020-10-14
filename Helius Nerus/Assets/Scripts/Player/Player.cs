using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static event System.Action<Player> PlayerHelathChanged = delegate { };
    public static event System.Action<Player> PlayerTookDamage = delegate { };
    public static event System.Action<Player> PlayerBeforeDie = delegate { };
    public static event System.Action<Player> PlayerDie = delegate { };
    public static event System.Action<Player> PlayerResurrection = delegate { };

	public static event System.Action<Player> PlayerUseBomb = delegate { };

    public PlayerParameters PlayerParameters
    {
        get => _playerParameters;
    }
	public Rigidbody2D Rigidbody2D
	{
		get => _rigidbody2D;
	}
	public PlayerWeapon[] PlayerWeapons
	{
		get => _weapons;
	}

	[SerializeField] private PlayerParameters _playerParametersSO = null;
    [SerializeField] private PlayerWeapon[] _weapons = null;
	[SerializeField] private ArtifactType[] _startArtifacts = null;
	
	private List<IPlayerArtifact> _artifacts = new List<IPlayerArtifact>();

    private PlayerParameters _playerParameters = null;
    private Rigidbody2D _rigidbody2D = null;

    private void Awake()
    {
        _playerParameters = _playerParametersSO.Clone();

        _rigidbody2D = GetComponent<Rigidbody2D>();

        _playerParameters.CurrentHealth = _playerParameters.MaxHealth;

		foreach (ArtifactType artifactType in _startArtifacts)
		{
			IPlayerArtifact artifact = ArtifactsCollection.GetArtifactByEnum(artifactType);
			artifact.OnPick(this);
			_artifacts.Add(artifact);
		}
	}

    private void OnDestroy()
    {
		for (int i = 0; i < _artifacts.Count; ++i)
		{
			_artifacts[i].OnDrop(this);
		}
	}

    private void Update()
    {
        if (Pause.Paused)
            return;

		for (int i = 0; i < _artifacts.Count; ++i)
		{
			_artifacts[i].OnTick(this);
		}
	}

    private void TakeDamage(int damage)
    {
        if (_rigidbody2D.simulated == false)
            return;

        if (damage >= 0)
        {
            _playerParameters.CurrentHealth -= damage;
            PlayerHelathChanged.Invoke(this);
        }

        if (_playerParameters.CurrentHealth <= 0)
        {
            PlayerBeforeDie.Invoke(this);

            if (_playerParameters.CurrentHealth <= 0)
            {
                PlayerDie.Invoke(this);
                Destroy(gameObject);
            }
            else
            {
				PlayerTookDamage.Invoke(this);
                PlayerResurrection.Invoke(this);
			}
		}
		else
		{
			PlayerTookDamage.Invoke(this);
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
            upgrade.UpgradeCharacter(this);
            return;
        }
    }

    public void IncrementHealth()
    {
        if (_playerParameters.CurrentHealth < _playerParameters.MaxHealth)
		{
            _playerParameters.CurrentHealth++;
			PlayerHelathChanged.Invoke(this);
		}
    }
}
