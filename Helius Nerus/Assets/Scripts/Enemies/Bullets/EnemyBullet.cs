using UnityEngine;

public class EnemyBullet : MonoBehaviour, IBulletMovement, IDealDamageToPlayer, IReturnableToPool
{
    [SerializeField] protected int _damage = 1;
	[SerializeField] protected BulletTypes _bulletType = BulletTypes.AngelBullet;

    protected float _speedMultiplier = 1.0f;
    protected Transform _transform = null;

	public Transform Transform
	{
		get => _transform;
	}

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
        BulletPoolsContainer.Instance.ReturnObjectToPool(_bulletType, gameObject);
    }

    public int GetMyDamage()
    {
        ReturnMeToPool(); // Cause it's one-hit bullet and will always return to pool after damaging
        return Damage;
    }
}