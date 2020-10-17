using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineProcessor : MonoBehaviour
{
	public static CoroutineProcessor Instance { get; private set; } = null;

	public static void ProcessArtifact(IPlayerArtifact artifact)
	{
		Instance.StartCoroutine(artifact.OnProc());
	}

	public static void ProcessModifier(IPlayerWeaponModifier modifier, PlayerBullet playerBullet)
	{
		Instance.StartCoroutine(modifier.OnProc(playerBullet));
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
}
