using System.Collections;
using UnityEngine;

public abstract class Boss : MonoBehaviour, IDealDamageToPlayer, ITakeDamageFromPlayer
{
    public static event System.Action<int> BossDied = delegate { };

    [SerializeField] protected Collider2D _collider = null;
    [SerializeField] protected float _maxHealth = 100;
    [Tooltip("Проценты(доли) хп, при которых происходит смена фазы")]
    [Range(0, 1)]
    [SerializeField] protected float[] _healthPercentageForPhases = new float[3];
    [SerializeField] protected float _delayBeforeFirstAction = 2.0f;
    [Tooltip("Сколько уровней открывает этот босс, если он является финальным")]
    [SerializeField] protected int _levelsOpened = 4;

    protected float _health = 100;
    protected BossPhase[] _phases = new BossPhase[3];
    protected int _currentPhase = 0;

    protected WaitForSeconds _waitForSeconds = null;

    public static Boss Instance
    {
        get;
        private set;
    } = null;
    public float Health
    {
        get => _health;
    }
    public int Damage { get; set; } = 1;

    protected void Awake()
    {
        Instance = this;
        _waitForSeconds = new WaitForSeconds(_delayBeforeFirstAction);

        _health = _maxHealth;
        _collider.enabled = false;

        StartCoroutine(LaunchBoss());
    }

    private IEnumerator LaunchBoss()
    {
        // Вставить сюда выползание босса на экран
        yield return _waitForSeconds;
        SetupPhases();
        _collider.enabled = true;
        _phases[0].StartPhase();
    }

    protected abstract void SetupPhases();

    protected void OnDestroy()
    {
        Instance = null;
    }

    protected void ChangePhase()
    {
        _phases[_currentPhase].StopPhase();
        _phases[++_currentPhase].StartPhase();
    }

    public int GetMyDamage()
	{
        return Damage;
	}

	public void TakeDamage(float damage)
	{
        _health -= damage;

        if (_health <= 0)
        {
            // die
            BossDied.Invoke(_levelsOpened);

            gameObject.SetActive(false);

            return;
        }

        if (_health / _maxHealth < _healthPercentageForPhases[_currentPhase])
        {
            //change phase
            ChangePhase();
        }
	}
}
