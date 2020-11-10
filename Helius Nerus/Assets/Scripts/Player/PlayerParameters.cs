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

    public static PlayerParameters DeserializeFromString(string s)
    {
        PlayerParameters playerParameters = PlayerParameters.CreateInstance<PlayerParameters>();

        string[] parametrs = s.Split(';');
        playerParameters.CurrentHealth = int.Parse(parametrs[0]);
        playerParameters._maxHealth = int.Parse(parametrs[1]);
        //playerParameters._invinsibilityTime = float.Parse(parametrs[2], );
        playerParameters._invinsibilityTime = System.Convert.ToSingle(parametrs[2]);
        playerParameters._bombsAmount = int.Parse(parametrs[3]);
        playerParameters._maxBombs = int.Parse(parametrs[4]);
        playerParameters._bombRadius = int.Parse(parametrs[5]);

        return playerParameters;
    }

    public string SerializeToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append(CurrentHealth);
        sb.Append(';');

        sb.Append(_maxHealth);
        sb.Append(';');

        sb.Append(_invinsibilityTime);
        sb.Append(';');

        sb.Append(_bombsAmount);
        sb.Append(';');

        sb.Append(_maxBombs);
        sb.Append(';');

        sb.Append(_bombRadius);

        return sb.ToString();
    }

    public PlayerParameters Clone()
    {
        return (PlayerParameters)MemberwiseClone();
    }
}
