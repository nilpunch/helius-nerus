using System.Collections;
using UnityEngine;

public class SprayModifier : PlayerWeaponModifier
{
	public const float WEAPON_MOVE_TIME = 1f;

    public override string MyEnumName => "SprayModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (SprayModifier)MemberwiseClone();
	}

	public override void OnPick(PlayerWeapon playerWeapon)
	{
		playerWeapon.StartCoroutine(OnWeaponProc(playerWeapon));
	}

	public IEnumerator OnWeaponProc(PlayerWeapon playerWeapon)
	{
		float startWeaponAngle = playerWeapon.WeaponParameters.WeaponAngle;
		float offsetAngle = 0f;
		bool goBack = false;

		while (true)
		{
			if (playerWeapon.IsNoSooting)
			{
				playerWeapon.WeaponParameters.WeaponAngle = startWeaponAngle;
				yield return null;
				continue;
			}

			float deltaAngle = playerWeapon.WeaponParameters.SpreadAngle * WEAPON_MOVE_TIME
                * TimeManager.PlayerDeltaTime;
			if (goBack == false)
			{
				if (offsetAngle < playerWeapon.WeaponParameters.SpreadAngle / 2f)
				{
					offsetAngle += deltaAngle;
				}
				else
				{
					goBack = true;
				}
			}
			else
			{
				if (offsetAngle > -playerWeapon.WeaponParameters.SpreadAngle / 2f)
				{
					offsetAngle -= deltaAngle;
				}
				else
				{
					goBack = false;
				}
			}
			playerWeapon.WeaponParameters.WeaponAngle = startWeaponAngle + offsetAngle;
			yield return null;
		}
	}
}