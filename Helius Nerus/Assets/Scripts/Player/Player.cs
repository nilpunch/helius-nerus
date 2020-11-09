using System.Collections.Generic;
using System.Text;
using System;
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            MidGameSaver.SavePlayerShip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            MidGameSaver.LoadPlayerShip();
        }
#endif
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

    public void LoadFromSavedData(int shipNumber, PlayerParameters parameters, string[] artifacts)
    {
        ShipNumber = shipNumber;
        _playerParameters = parameters.Clone();

        for (int i = 0; i < _artifacts.Count; ++i)
        {
            _artifacts[i].OnDrop();
        }
        _artifacts.Clear();

        foreach (string s in artifacts)
        {
            ArtifactType artifactType = (ArtifactType)Enum.Parse(typeof(ArtifactType), s);

            PlayerArtifact artifact = PlayerArtifactContainer.Instance.GetArtifact(artifactType);
            PlayerArtifactContainer.Instance.RemoveArtifactFromPoolIfExists(artifactType);

            artifact.OnPick();
            _artifacts.Add(artifact);
        }
    }
}

public class MidGameSaver
{
    private class SavedData
    {
        public int _shipNumber;
        public string _playerParametrs;
        public string _playerArtifacts;

        public SavedData()
        {
        }

        public SavedData(int a)
        {
            _shipNumber = Player.ShipNumber;
            _playerParametrs = JsonUtility.ToJson(Player.PlayerParameters);

            StringBuilder sb = new StringBuilder();
            foreach (PlayerArtifact artifact in Player.PlayerArtifacts)
            {
                sb.Append(artifact.MyEnumName);
                sb.Append(',');
            }
            _playerArtifacts = sb.ToString();
            sb.Clear();
        }
    }

    public static void SavePlayerShip()
    {
        SavedData data = new SavedData(1);
        // ДЖсон ютилити туда сюда плейерпрефс блаблабла
        string dataAsJson = JsonUtility.ToJson(data);


        PlayerPrefs.SetString("PlayerShip", dataAsJson);
#if UNITY_EDITOR
        Debug.Log(dataAsJson);
#endif
    }

    public static void LoadPlayerShip()
    {
        if (PlayerPrefs.HasKey("PlayerShip") == false)
        {
#if UNITY_EDITOR
            Debug.Log("Player ship playerprefs not exist");
#endif
            return;
        }
        SavedData data = JsonUtility.FromJson<SavedData>(PlayerPrefs.GetString("PlayerShip"));

        

        //Player.Instance.LoadFromSavedData(data._shipNumber, parameters, data._playerArtifacts.Split(','));
#if UNITY_EDITOR
        Debug.Log("Yes");
#endif
    }
}
