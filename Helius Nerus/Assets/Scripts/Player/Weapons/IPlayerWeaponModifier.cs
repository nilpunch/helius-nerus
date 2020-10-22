using System.Collections;

public interface IPlayerWeaponModifier
{
	void OnPick(PlayerWeapon playerWeapon);
	void OnDrop(PlayerWeapon playerWeapon);
	void OnBulletEnable(PlayerBullet playerBullet);
	void OnWeaponShoot(PlayerWeapon playerBullet);
	void OnHit(PlayerBullet playerBullet, Enemy enemy);
    void OnDestroy(PlayerBullet playerBullet);
	IEnumerator OnProc(PlayerBullet playerBullet);
	IPlayerWeaponModifier Clone();
}