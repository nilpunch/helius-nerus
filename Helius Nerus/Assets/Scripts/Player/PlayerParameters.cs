using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParams", menuName = "ScriptableObjects/Player parametrs", order = 5)]
public class PlayerParameters : ScriptableObject
{
	[Tooltip("Максимальное количество здоровья")]
	[SerializeField] private int _maxHealth = 4;
	[Tooltip("Время неуязвимости")]
	[SerializeField] private float _invinsibilityTime = 1.0f;

	public int CurrentHealth { get; set; } = 0;
	public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
	public float InvinsibilityTime { get => _invinsibilityTime; set => _invinsibilityTime = value; }

    public PlayerParameters Clone()
    {
        return (PlayerParameters)MemberwiseClone();
    }
}
