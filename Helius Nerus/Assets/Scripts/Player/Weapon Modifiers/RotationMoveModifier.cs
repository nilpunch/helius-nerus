using System.Collections;
using UnityEngine;

class RotationMoveModifier : PlayerWeaponModifier
{
	public const float MAX_ANGLE_INCREMENT = 5.0f;

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
            if (Pause.Paused)
            {
                yield return null;
                continue;
            }
            playerBullet.Transform.Rotate(0.0f, 0.0f, angleIncrement * Time.deltaTime * playerBullet.BulletParameters.SpeedMultiplier, Space.Self);
			yield return null;
		}
	}
}
