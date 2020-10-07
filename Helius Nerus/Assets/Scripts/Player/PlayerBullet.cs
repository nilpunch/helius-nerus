using UnityEngine;

public class PlayerBullet : MonoBehaviour, IReturnableToPool
{
    public float SpeedMultiplier
    {
        set => _speedMultiplier = value;
    }
    private float _speedMultiplier = 1.0f;
    private Transform _transform = null;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * Time.deltaTime * _speedMultiplier, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Нанести урон еще надо
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(GetComponent<DealContactDamage>().Damage);
            // Какое-то условие по прочности
            if (true)
                ReturnMeToPool();
        }
    }

    public void ReturnMeToPool()
    {
        BulletPoolsContainer.Instance.ReturnObjectToPool(BulletTypes.PlayerBullet, gameObject);
    }
}
