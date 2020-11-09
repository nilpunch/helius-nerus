using UnityEngine;

public abstract class PlayerWeaponModifier
{
    public abstract string MyEnumName
    {
        get;
    }

	public virtual void OnPick(PlayerWeapon playerWeapon) { }
	public virtual void OnDrop(PlayerWeapon playerWeapon) { }
	public virtual void OnBulletEnable(PlayerBullet playerBullet) { }
	public virtual void OnWeaponShoot(PlayerWeapon playerWeapon) { }
	public virtual void OnHit(PlayerBullet playerBullet, GameObject target) { }
	public virtual void OnBulletDestroy(PlayerBullet playerBullet) { }
	public abstract PlayerWeaponModifier Clone();
}