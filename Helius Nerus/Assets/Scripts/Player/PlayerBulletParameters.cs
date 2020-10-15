public class PlayerBulletParameters
{
	public float SpeedMultiplier { get; set; } = 1.0f;
	public int Durability { get; set; } = 1;
	public float Damage { get; set; } = 1.0f;

	public PlayerBulletParameters Clone()
	{
		return (PlayerBulletParameters)MemberwiseClone();
	}
}
