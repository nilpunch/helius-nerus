using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollision : MonoBehaviour
{
	[SerializeField] private Player _player = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDealDamageToPlayer dealDamageToPlayer = (collision.gameObject.GetComponent(typeof(IDealDamageToPlayer)) as IDealDamageToPlayer);
		if (dealDamageToPlayer != null)
		{
			_player.TakeDamage(dealDamageToPlayer.GetMyDamage());
			return;
		}
		UpgradeBase upgrade = collision.GetComponent<UpgradeBase>();
		if (upgrade != null)
		{
			upgrade.UpgradeCharacter(_player);
			return;
		}
	}
}
