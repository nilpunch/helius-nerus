using System.Collections;
using UnityEngine;

public class StraightMoveModifier : PlayerWeaponModifier
{
    public override PlayerWeaponModifier Clone()
	{
		return (StraightMoveModifier)MemberwiseClone();
	}

	public override void OnBulletEnable(PlayerBullet playerBullet)
	{
		playerBullet.StartCoroutine(OnBulletProc(playerBullet));
	}

	public IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		while (playerBullet.gameObject.activeSelf)
		{
            playerBullet.Transform.Translate(Vector3.up * 
                playerBullet.BulletParameters.SpeedMultiplier * TimeManager.PlayerDeltaTime, Space.Self);
			yield return null;
		}
	}
}
