using System.Collections;

public class ExplosionModifier : IPlayerWeaponModifier
{
    public string Description
    {
        get => "explosionDescription";
    }
    public IPlayerWeaponModifier Clone()
	{
		return (ExplosionModifier)MemberwiseClone();
	}

	public void OnBulletDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnBulletEnable(PlayerBullet playerBullet)
	{
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public IEnumerator OnProc(PlayerBullet playerBullet)
	{
		yield break;
	}

	public void OnPick(PlayerWeapon playerWeapon)
	{
	}

	public void OnDrop(PlayerWeapon playerWeapon)
	{
	}

	public void OnWeaponShoot(PlayerWeapon playerBullet)
	{
	}
}
