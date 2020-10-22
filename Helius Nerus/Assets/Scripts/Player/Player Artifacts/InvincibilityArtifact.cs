using System.Collections;
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
        // Мне не нравится эта строка. Лучше уже кэшануть и в свойство
        _renderer = Player.SpriteRenderer;
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
		Player.CollideWithDamageDealer = false;
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

		Player.CollideWithDamageDealer = true;
		_renderer.enabled = true;
	}
}
