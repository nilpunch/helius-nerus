using System.Collections;
using UnityEngine;

public class SplitInTwoModifier : PlayerWeaponModifier
{
	public const float COLLISION_DISABLE_TIME = 0.1f;
	public const float DAMAGE_REDUСTION_COEFFICIENT = 2.0f;
	public const float SPLIT_ANGLE = 45f;

	private WaitForSeconds _waiter = new WaitForSeconds(COLLISION_DISABLE_TIME);

    public override ModifierType MyEnumValue => ModifierType.SplitInTwoModifier;

    public override PlayerWeaponModifier Clone()
	{
		return (SplitInTwoModifier)MemberwiseClone();
	}

	public override void OnBulletDestroy(PlayerBullet playerBullet)
	{
		PlayerBullet leftBullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet).GetComponent<PlayerBullet>();
		PlayerBullet rightBullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet).GetComponent<PlayerBullet>();

		leftBullet.SetModifiers(playerBullet.Modifiers);
		leftBullet.BulletParameters.SpeedMultiplier = playerBullet.BulletParameters.SpeedMultiplier;
		leftBullet.BulletParameters.Damage = playerBullet.BulletParameters.Damage / DAMAGE_REDUСTION_COEFFICIENT;
		leftBullet.Transform.position = playerBullet.Transform.position;
		leftBullet.Transform.localScale = playerBullet.transform.localScale;
		leftBullet.Transform.localEulerAngles = new Vector3(0f, 0f, playerBullet.Transform.localEulerAngles.z - SPLIT_ANGLE);
		leftBullet.OnBulletEnable();

		rightBullet.SetModifiers(playerBullet.Modifiers);
		rightBullet.BulletParameters.SpeedMultiplier = playerBullet.BulletParameters.SpeedMultiplier;
		rightBullet.BulletParameters.Damage = playerBullet.BulletParameters.Damage / DAMAGE_REDUСTION_COEFFICIENT;
		rightBullet.Transform.position = playerBullet.Transform.position;
		rightBullet.Transform.localScale = playerBullet.transform.localScale;
		rightBullet.Transform.localEulerAngles = new Vector3(0f, 0f, playerBullet.Transform.localEulerAngles.z + SPLIT_ANGLE);
		rightBullet.OnBulletEnable();

		leftBullet.StartCoroutine(OnBulletProc(leftBullet));
		rightBullet.StartCoroutine(OnBulletProc(rightBullet));
	}

	private IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		playerBullet.CollideWithEnemies = false;
		yield return _waiter;
		playerBullet.CollideWithEnemies = true;
	}
}
