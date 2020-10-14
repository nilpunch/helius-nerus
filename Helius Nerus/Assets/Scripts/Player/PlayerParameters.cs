using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParams", menuName = "ScriptableObjects/Player parametrs", order = 5)]
public class PlayerParameters : ScriptableObject
{
	[Tooltip("Максимальное количество здоровья")]
	[SerializeField] private int _maxHealth = 4;
	[Tooltip("Время неуязвимости")]
	[SerializeField] private float _invinsibilityTime = 1.0f;
	[Tooltip("Стартовое количество бомб")]
	[SerializeField] private int _bombsAmount = 0;
	[Tooltip("Максимальное количество бомб")]
	[SerializeField] private int _maxBombs = 0;
	[Tooltip("Радуис взрыва бомб")]
	[SerializeField] private float _bombRadius = 0;

	public int CurrentHealth { get; set; } = 0;
	public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
	public float InvinsibilityTime { get => _invinsibilityTime; set => _invinsibilityTime = value; }
	public int BombsAmount { get => _bombsAmount; set => _bombsAmount = value; }
	public int MaxBombs { get => _maxBombs; set => _maxBombs = value; }
	public float BombRadius { get => _bombRadius; set => _bombRadius = value; }

	public PlayerParameters Clone()
    {
        return (PlayerParameters)MemberwiseClone();
    }
}
