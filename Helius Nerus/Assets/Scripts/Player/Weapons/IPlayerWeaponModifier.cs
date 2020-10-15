public interface IPlayerWeaponModifier
{
	void OnEnable(PlayerBullet playerBullet);
	void OnHit(PlayerBullet playerBullet, Enemy enemy);
    void OnDestroy(PlayerBullet playerBullet);
	void OnTick(PlayerBullet playerBullet);
	IPlayerWeaponModifier Clone();
}