using System.Collections;
using UnityEngine;

class RotationMoveModifier : IPlayerWeaponModifier
{
	private const float MAX_ANGLE_INCREMENT = 5.0f;

	public IPlayerWeaponModifier Clone()
	{
		return (RotationMoveModifier)MemberwiseClone();
	}

	public void OnDestroy(PlayerBullet playerBullet)
	{
	}

	public void OnShoot(PlayerBullet playerBullet)
	{
		CoroutineProcessor.ProcessModifier(this, playerBullet);
	}

	public void OnHit(PlayerBullet playerBullet, Enemy enemy)
	{
	}

	public IEnumerator OnProc(PlayerBullet playerBullet)
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
