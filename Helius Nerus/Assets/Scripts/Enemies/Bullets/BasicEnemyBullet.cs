using UnityEngine;

public class BasicEnemyBullet : MonoBehaviour, IBulletMovement, IDealDamageToPlayer, IReturnableToPool
{
    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    [SerializeField] protected int _damage = 1;

    protected float _speedMultiplier = 1.0f;
    protected Transform _transform = null;
    protected bool _paused = false;

    protected virtual void Awake()
    {
        _transform = transform;

        Pause.GamePaused += Pause_GamePaused;
        Pause.GameUnpaused += Pause_GameUnpaused;
    }

    private void OnDestroy()
    {
        Pause.GamePaused -= Pause_GamePaused;
        Pause.GameUnpaused -= Pause_GameUnpaused;
    }

    private void Pause_GameUnpaused()
    {
        _paused = false;
    }

    private void Pause_GamePaused()
    {
        _paused = true;
    }

    public void ReturnMeToPool()
    {
        BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.StraightMove, gameObject);
    }

    public int GetMyDamage()
    {
        ReturnMeToPool(); // Cause it's one-hit bullet and will always return to pool after damaging
        return Damage;
    }
}