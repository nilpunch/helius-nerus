using UnityEngine;

public class BasicEnemyBullet : MonoBehaviour, IBulletMovement, IDealDamageToPlayer, IReturnableToPool
{
    [SerializeField] protected int _damage = 1;

    protected float _speedMultiplier = 1.0f;
    protected Transform _transform = null;

    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    protected virtual void Awake()
    {
        _transform = transform;
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