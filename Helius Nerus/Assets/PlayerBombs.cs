using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerBombs : MonoBehaviour
{
	[SerializeField] private Player _playerScript = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_playerScript.PlayerParameters.BombsAmount >= 0)
			{

			}
		}
    }
}
