﻿using System.Collections;
using UnityEngine;

class InvincibilityArtifact : IPlayerArtifact
{
	private const float EFFECT_TIME_SCALE = 1.1f;

	private SpriteRenderer _renderer;

	public IPlayerArtifact Clone()
	{
		return (InvincibilityArtifact)this.MemberwiseClone();
	}

	public void OnPick()
	{
		Player.PlayerTookDamage += Player_PlayerTookDamage;
		_renderer = Player.Instance.GetComponent<SpriteRenderer>();
	}

	public void OnDrop()
	{
		Player.PlayerTookDamage -= Player_PlayerTookDamage;
	}

	private void Player_PlayerTookDamage()
	{
		CoroutineProcessor.ProcessArtifact(this);
	}

	public IEnumerator OnProc()
	{
		float invinsibilityLeft = Player.PlayerParameters.InvinsibilityTime;
		Player.Rigidbody2D.simulated = false;
		float effectToggleTime = invinsibilityLeft / EFFECT_TIME_SCALE;
		_renderer.enabled = false;
		
		while (invinsibilityLeft > 0f)
		{
			if (Pause.Paused)
				yield return null;

			invinsibilityLeft -= Time.deltaTime;
			if (invinsibilityLeft < effectToggleTime)
			{
				effectToggleTime = invinsibilityLeft / EFFECT_TIME_SCALE;
				_renderer.enabled = !_renderer.enabled;
			}
			yield return null;
		}

		Player.Rigidbody2D.simulated = true;
		_renderer.enabled = true;
	}
}