public class BlankModifier : IPlayerWeaponModifier
{
    public IPlayerWeaponModifier Clone()
    {
        return (IPlayerWeaponModifier)this.MemberwiseClone();
    }

	public void OnDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnEnable(PlayerBullet playerBullet)
	{
	}

	public void OnHit(PlayerBullet playerBullet)
	{
	}

	public void OnTick(PlayerBullet playerBullet)
	{
	}
}