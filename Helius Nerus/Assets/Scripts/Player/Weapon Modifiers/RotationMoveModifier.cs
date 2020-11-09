using System.Collections;
using UnityEngine;

class RotationMoveModifier : PlayerWeaponModifier
{
	public const float MAX_ANGLE_INCREMENT = 5.0f;

    public override string MyEnumName => "RotationMoveModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (RotationMoveModifier)MemberwiseClone();
	}

	public override void OnBulletEnable(PlayerBullet playerBullet)
	{
		playerBullet.StartCoroutine(OnBulletProc(playerBullet));
	}

	public IEnumerator OnBulletProc(PlayerBullet playerBullet)
	{
		float angleIncrement = Random.Range(-MAX_ANGLE_INCREMENT, MAX_ANGLE_INCREMENT);

		while (playerBullet.gameObject.activeSelf)
		{
            playerBullet.Transform.Rotate(0.0f, 0.0f, angleIncrement * 
                playerBullet.BulletParameters.SpeedMultiplier * TimeManager.PlayerDeltaTime, Space.Self);
			yield return null;
		}
	}
}
