public interface IPlayerWeaponModifier
{
	void OnPick(PlayerWeapon playerWeapon);
	void OnDrop(PlayerWeapon playerWeapon);
	void OnBulletEnable(PlayerBullet playerBullet);
	void OnWeaponShoot(PlayerWeapon playerWeapon);
	void OnHit(PlayerBullet playerBullet, Enemy enemy);
    void OnBulletDestroy(PlayerBullet playerBullet);
	IPlayerWeaponModifier Clone();
}