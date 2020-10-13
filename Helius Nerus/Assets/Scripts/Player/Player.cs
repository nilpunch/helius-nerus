using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static event System.Action<Player> PlayerTookDamage = delegate { };
    public static event System.Action<Player> PlayerBeforeDie = delegate { };
    public static event System.Action<Player> PlayerDie = delegate { };
    public static event System.Action<Player> PlayerResurrection = delegate { };

    public PlayerParameters PlayerParameters
    {
        get => _playerParameters;
    }

    [SerializeField] private PlayerParameters _playerParametersSO = null;
    [SerializeField] private PlayerWeapon[] _weapons;

    private PlayerParameters _playerParameters = null;
    private float _invinsibilityLeft = 0.0f;
    private bool _isInvincible = false;

    private Rigidbody2D _rigidbody = null;

    private void Awake()
    {
        _playerParameters = _playerParametersSO.Clone();

        _rigidbody = GetComponent<Rigidbody2D>();

        _playerParameters.CurrentHealth = _playerParameters.MaxHealth;
    } 

    private void Update()
    {
        if (_isInvincible)
        {
            _invinsibilityLeft -= Time.deltaTime;
            if (_invinsibilityLeft <= 0.0f)
            {
                _isInvincible = false;
                _rigidbody.simulated = true;
#if UNITY_EDITOR
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
#endif
            }
        }
    }

    private void BecomeInvincible()
    {
        _invinsibilityLeft = _playerParameters.InvinsibilityTime;
        _isInvincible = true;
        _rigidbody.simulated = false; // Disable rigidbody (collider), so player is not able to take damej
                                      // For demonstration purposes
#if UNITY_EDITOR
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
#endif
    }

    private void TakeDamage(int damage)
    {
        if (_isInvincible)
            return;

        if (damage >= 0)
        {
            _playerParameters.CurrentHealth -= damage;
            PlayerTookDamage.Invoke(this);
            BecomeInvincible();
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
                PlayerResurrection.Invoke(this);
            }
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
            _playerParameters.CurrentHealth++;
    }
}
