using UnityEngine;

public class DealContactDamage : MonoBehaviour
{
    private float _damage = 1;

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
}
