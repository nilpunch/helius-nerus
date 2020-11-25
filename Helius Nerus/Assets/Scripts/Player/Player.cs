using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action PlayerHealthChanged = delegate { };
    public static event Action PlayerTookDamage = delegate { };
    public static event Action PlayerBeforeDie = delegate { };
    public static event Action PlayerDie = delegate { };
    public static event Action PlayerResurrection = delegate { };

    [SerializeField] private PlayerMovement _playerMovement = null;
    [Space]
    [SerializeField] private PlayerParameters _playerParametersSO = null;
    [SerializeField] private PlayerWeapon[] _weapons = null;
    [SerializeField] private ArtifactType[] _startArtifacts = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private SpriteRenderer _renderer = null;

    private bool _collideWithDamageDealers = true;

    private List<PlayerArtifact> _artifacts = new List<PlayerArtifact>();
    private PlayerParameters _playerParameters = null;

    public static Player Instance
    {
        get;
        private set;
    }
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
    public static List<PlayerArtifact> PlayerArtifacts
    {
        get => Instance._artifacts;
    }
    public static bool CollideWithDamageDealers
    {
        get => Instance._collideWithDamageDealers;
        set => Instance._collideWithDamageDealers = value;
    }
    public static int ShipNumber
    {
        get;
        set;
    } = 0;
    public bool IsNotMoving
    {
        get;
        set;
    } = false;
    public bool IsNotShooting
    {
        get;
        set;
    } = false;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _playerMovement.Init();
        RestartPlayer();
    }

    private void Update()
    {
        if (IsNotMoving)
            return;
        _playerMovement.Update();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _artifacts.Count; ++i)
        {
            _artifacts[i].OnDrop();
        }
    }

    public void RestartPlayer()
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
            //IPlayerArtifact artifact = ArtifactsCollection.GetArtifactByEnum(artifactType);
            PlayerArtifact artifact = PlayerArtifactContainer.Instance.GetArtifact(artifactType);
            artifact.OnPick();
            _artifacts.Add(artifact);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDealDamageToPlayer dealDamageToPlayer = (collision.gameObject.GetComponent(typeof(IDealDamageToPlayer)) as IDealDamageToPlayer);
        if (dealDamageToPlayer != null && _collideWithDamageDealers)
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
            PlayerHealthChanged.Invoke();
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
            PlayerHealthChanged.Invoke();
        }
    }

    public void LoadFromSavedData(PlayerParameters parameters, string[] artifacts, List<string> weaponParams, List<string> weaponMods)
    {
        if (weaponParams.Count != _weapons.Length)
        {
#if UNITY_EDITOR
            Debug.LogError("Количество пушек в сохранении и на корабле не совпадает!");
#endif
            return;
        }

        _playerParameters = parameters;

        for (int i = 0; i < _artifacts.Count; ++i)
        {
            _artifacts[i].OnDrop();
        }
        _artifacts.Clear();

        foreach (string s in artifacts)
        {
#if UNITY_EDITOR
            Debug.Log("Player artifact - " + s);
#endif

            ArtifactType artifactType = (ArtifactType)Enum.Parse(typeof(ArtifactType), s);

            PlayerArtifact artifact = PlayerArtifactContainer.Instance.GetArtifact(artifactType);
            PlayerArtifactContainer.Instance.RemoveArtifactFromPoolIfExists(artifactType);

            artifact.OnPick();
            _artifacts.Add(artifact);
        }

        for (int i = 0; i < _weapons.Length; ++i)
        {
            _weapons[i].LoadFromSavedData(weaponParams[i], weaponMods[i]);
        }
    }

    public void SimulateDie()
    {
        PlayerDie.Invoke();
        gameObject.SetActive(false);
    }
}