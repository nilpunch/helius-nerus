using System.Collections.Generic;
using UnityEngine;

class InvincibilityArtifact : IPlayerArtifact
{
	private bool _isInvincible = false;
	private float _invinsibilityLeft = 0.0f;

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
#if UNITY_EDITOR
		player.GetComponent<SpriteRenderer>().color = Color.green;
#endif
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
#if UNITY_EDITOR
				player.GetComponent<SpriteRenderer>().color = Color.blue;
#endif
			}
		}
	}
}
