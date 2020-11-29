using DG.Tweening;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour, ITakeDamageFromPlayer
{
    public static event System.Action TutorialEnemyDied = delegate { };

    [SerializeField] private float _health = 10;

    private bool _isDead = false;


    private void OnEnable()
    {
        transform.DOMoveY(3f, 3f);
    }


    private void OnDisable()
    {
        TutorialEnemyDied.Invoke();
    }


    public virtual void OnUpdate() { }

    public void TakeDamage(float damage)
    {
        if (_isDead)
            return;
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        UpgrageCollection.Instance.GetRandomUpgrade().transform.position = transform.position;

        Destroy(gameObject);
    }
}
