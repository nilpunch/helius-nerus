using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBombs : MonoBehaviour
{
	[SerializeField] private Player _player = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_player.PlayerParameters.BombsAmount >= 0)
			{
				// Do bombs
			}
		}
    }
}
