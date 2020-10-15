using System.Collections.Generic;
using UnityEngine;

class InvincibilityArtifact : IPlayerArtifact
{
	private const float EFFECT_TIME_SCALE = 1.1f;
	private bool _isInvincible = false;
	private float _invinsibilityLeft = 0.0f;
	private float _effectToggleTime = 0.0f;

	public IPlayerArtifact Clone()
	{
		return (InvincibilityArtifact)this.MemberwiseClone();
	}

	public void OnDrop(Player player)
	{
		Player.PlayerTookDamage -= Player_PlayerTakeDamage;
	}

	public void OnPick(Player player)
	{
		Player.PlayerTookDamage += Player_PlayerTakeDamage;
	}

	private void Player_PlayerTakeDamage(Player player)
	{
		_invinsibilityLeft = player.PlayerParameters.InvinsibilityTime;
		player.Rigidbody2D.simulated = false;
		_isInvincible = true;

		// Setup invincibility effect
		_effectToggleTime = _invinsibilityLeft / EFFECT_TIME_SCALE;
		player.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void OnTick(Player player)
	{
		if (Pause.Paused)
			return;

		if (_isInvincible)
		{
			_invinsibilityLeft -= Time.deltaTime;
			if (_invinsibilityLeft <= 0.0f) 
			{
				_isInvincible = false;
				player.Rigidbody2D.simulated = true;

				// Disable invincibility effect
				player.GetComponent<SpriteRenderer>().enabled = true;
			}
			else 
			{
				// Handle invincibility effect
				if (_invinsibilityLeft < _effectToggleTime)
				{
					_effectToggleTime = _invinsibilityLeft / EFFECT_TIME_SCALE;
					player.GetComponent<SpriteRenderer>().enabled = !player.GetComponent<SpriteRenderer>().enabled;
				}
			}
		}
	}
}
