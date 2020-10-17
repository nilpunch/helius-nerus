using System.Collections;

public interface IPlayerWeaponModifier
{
	void OnShoot(PlayerBullet playerBullet);
	void OnHit(PlayerBullet playerBullet, Enemy enemy);
    void OnDestroy(PlayerBullet playerBullet);
	IEnumerator OnProc(PlayerBullet playerBullet);
	IPlayerWeaponModifier Clone();
}