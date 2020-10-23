using System.Collections;

public class HomingModifier : IPlayerWeaponModifier
{
    public string Description
    {
        get => "homingDescription";
    }
    public IPlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
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
