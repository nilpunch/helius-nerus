public interface IPlayerWeaponModifier
{
    void OnHit(PlayerBullet playerBullet);
    void OnDestroy(PlayerBullet playerBullet);
    void OnEnable(PlayerBullet playerBullet);
    void OnTick(PlayerBullet playerBullet);
    IPlayerWeaponModifier Clone();
}