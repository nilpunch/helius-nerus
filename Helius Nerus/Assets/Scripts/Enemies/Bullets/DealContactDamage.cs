using UnityEngine;

public class DealContactDamage : MonoBehaviour
{
    public int Damage
    {
        set => _damage = value;
    }

    private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to player
        // Destroy itself
        Destroy(gameObject);
    }
}
