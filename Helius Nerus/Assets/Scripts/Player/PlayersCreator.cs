using UnityEngine;

public class PlayersCreator : MonoBehaviour
{
    [SerializeField] private Player[] _playerShips = null;
    private Player _currentPlayer = null;
    private int _currentPlayerIndex = -1;

    private void Awake()
    {
        ChangePlayer();
    }

    public void ChangePlayer()
    {
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= _playerShips.Length)
            _currentPlayerIndex = 0;

        //if (_currentPlayer != null)
        //    Destroy(_currentPlayer);

        _currentPlayer = Instantiate(_playerShips[_currentPlayerIndex]);
        _currentPlayer.IsStaticAndNoShooting = true;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePlayer();
        }
    }
#endif
}
