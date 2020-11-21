using UnityEngine;

public class Boss : MonoBehaviour, IDealDamageToPlayer, ITakeDamageFromPlayer
{
	public int Damage { get; set; }

	public int GetMyDamage()
	{
		throw new System.NotImplementedException();
	}

	public void TakeDamage(float damage)
	{
		throw new System.NotImplementedException();
	}
}
