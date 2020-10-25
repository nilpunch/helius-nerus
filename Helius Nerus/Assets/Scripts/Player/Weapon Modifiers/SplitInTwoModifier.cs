using System.Collections;
using UnityEngine;

public class SplitInTwoModifier : IPlayerWeaponModifier
{
	public const float COLLISION_DISABLE_TIME = 0.1f;
	public const float DAMAGE_REDUСTION_COEFFICIENT = 2.0f;
	public const float SPLIT_ANGLE = 45f;

	private WaitForSeconds _waiter = new WaitForSeconds(COLLISION_DISABLE_TIME);

    public string Description
    {
        get => "splitInTwoDescription";
    }

    public IPlayerWeaponModifier Clone()
	{
		return (SplitInTwoModifier)MemberwiseClone();
	}

	public void OnBulletDestroy(PlayerBullet playerBullet)
	{
		if (playerBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier] > 0)
		{
			playerBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier] -= 1;

			PlayerBullet leftBullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet).GetComponent<PlayerBullet>();
			PlayerBullet rightBullet = BulletPoolsContainer.Instance.GetObjectFromPool(BulletTypes.PlayerBullet).GetComponent<PlayerBullet>();

			leftBullet.SetModifiers(playerBullet.Modifiers);
			leftBullet.BulletParameters.SpeedMultiplier = playerBullet.BulletParameters.SpeedMultiplier;
			leftBullet.BulletParameters.Damage = playerBullet.BulletParameters.Damage / DAMAGE_REDUСTION_COEFFICIENT;
			leftBullet.Transform.position = playerBullet.Transform.position;
			leftBullet.Transform.localScale = Vector3.one * playerBullet.transform.localScale.x;
			leftBullet.Transform.localEulerAngles = new Vector3(0f, 0f, playerBullet.Transform.localEulerAngles.z - SPLIT_ANGLE);
			leftBullet.OnBulletEnable();

			leftBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier] = playerBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier];

			rightBullet.SetModifiers(playerBullet.Modifiers);
			rightBullet.BulletParameters.SpeedMultiplier = playerBullet.BulletParameters.SpeedMultiplier;
			rightBullet.BulletParameters.Damage = playerBullet.BulletParameters.Damage / DAMAGE_REDUСTION_COEFFICIENT;
			rightBullet.Transform.position = playerBullet.Transform.position;
			rightBullet.Transform.localScale = Vector3.one * playerBullet.transform.localScale.x;
			rightBullet.Transform.localEulerAngles = new Vector3(0f, 0f, playerBullet.Transform.localEulerAngles.z + SPLIT_ANGLE);
			rightBullet.OnBulletEnable();

			rightBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier] = playerBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier];

			CoroutineProcessor.Instance.StartCoroutine(OnBulletProc(leftBullet));
			CoroutineProcessor.Instance.StartCoroutine(OnBulletProc(rightBullet));
		}
	}

	public void OnBulletEnable(PlayerBullet playerBullet)
	{
		if (playerBullet.ModifiersProcCount.ContainsKey(ModifierType.SplitInTwoModifier) == false)
		{
			playerBullet.ModifiersProcCount.Add(ModifierType.SplitInTwoModifier, 1);
		}
		else
		{
			playerBullet.ModifiersProcCount[ModifierType.SplitInTwoModifier] += 1;
		}
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		playerBullet.CollideWithEnemies = false;
		yield return _waiter;
		playerBullet.CollideWithEnemies = true;
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
