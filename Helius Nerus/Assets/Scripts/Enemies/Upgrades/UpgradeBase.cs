using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour
{
    public int Weight
    {
        get => _weight;
    }
	public float Amount
	{
		get => _amount;
	}

    [Tooltip("Вес для генератора и балансировки")]
    [SerializeField] private int _weight = 0;
    [Tooltip("Изменяемый параметр в количестве (или другой параметре)")]
    [SerializeField] private float _amount = 1.0f;

    public abstract void UpgradeCharacter();
}