using UnityEngine;

public class LevelsChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels = null;

    private GameObject _current = null;
    private int _currentLevel = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnNextLevel();
        }
    }

    private void SpawnNextLevel()
    {
        //if (_current != null)
        //    Destroy(_current);

        ++_currentLevel;
        if (_currentLevel >= _levels.Length)
            _currentLevel = 0;

        _current = Instantiate(_levels[_currentLevel]);
    }

    public void ChangeLevel()
    {
        SpawnNextLevel();
    }
}
