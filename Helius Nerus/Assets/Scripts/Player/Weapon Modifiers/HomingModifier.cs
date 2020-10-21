using System.Collections;

public class HomingModifier : IPlayerWeaponModifier
{
	public IPlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}

	public void OnDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnShoot(PlayerBullet playerBullet)
	{
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public IEnumerator OnProc(PlayerBullet playerBullet)
	{
        yield break;
	}
}
