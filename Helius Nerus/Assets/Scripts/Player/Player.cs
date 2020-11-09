using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static event System.Action PlayerHealthChanged = delegate { };
    public static event System.Action PlayerTookDamage = delegate { };
    public static event System.Action PlayerBeforeDie = delegate { };
    public static event System.Action PlayerDie = delegate { };
    public static event System.Action PlayerResurrection = delegate { };

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
    /// <summary>
    /// Номер корабля для сохранений
    /// </summary>

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
}

public class MidGameSaver
{
    private class SavedData
    {
        public int _shipNumber;
        public PlayerParameters _playerParameters;
        public List<PlayerArtifact> _playerArtifacts;
        public PlayerWeapon[] _playerWeapons;
        public int _score;

        public List<PlayerWeaponsParametrs> _playerWeaponsParametrs;

        public List<string> _playerWeaponsModifiersValues;

        public SavedData()
        {
            _shipNumber = Player.ShipNumber;
            _playerParameters = Player.PlayerParameters;
            _playerArtifacts = Player.PlayerArtifacts;
            _playerWeapons = Player.PlayerWeapons;
            _score = ScoreCounter.Score;

            foreach(PlayerWeapon playerWeapon in _playerWeapons)
            {
                _playerWeaponsParametrs.Add(playerWeapon.WeaponParameters);
            }

            StringBuilder sb = new StringBuilder();
            foreach(PlayerWeapon playerWeapon in _playerWeapons)
            {
                // Мы каким-то образом получаем строки из параметра
                foreach(PlayerWeaponModifier modifier in playerWeapon.WeaponModifiers)
                {
                    // Пока поставим тут ТуСтринг, потом я поговорю за параметры конст статик
                    sb.Append(modifier.ToString());
                    sb.Append(',');
                }
                _playerWeaponsModifiersValues.Add(sb.ToString());
                sb.Clear();

                // Потом делаем стринг.сплит и получаем массив строк
                // Делаем Энам.Парсе и получаем наш массив энамов

                // Сейм штуку надо сделать для артефактов корабля
            }
        }
    }

    public static void SavePlayerShip()
    {
        SavedData data = new SavedData();
        // ДЖсон ютилити туда сюда плейерпрефс блаблабла
    }
}
