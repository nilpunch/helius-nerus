using UnityEngine;

public class DealContactDamage : MonoBehaviour
{
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _damage = 1;
}
