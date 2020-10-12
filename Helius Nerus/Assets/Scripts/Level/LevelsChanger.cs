using UnityEngine;

public class LevelsChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels = null;

    private int _currentLevel = -1;
    private GameObject _current = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnNextLevel();
        }
    }

    private void SpawnNextLevel()
    {
        ++_currentLevel;
        if (_currentLevel >= _levels.Length)
            _currentLevel = 0;


        _current = GameObject.Instantiate(_levels[_currentLevel]);
        _current.transform.position = Vector3.zero;
        _current.name = "LEVEL";

#if UNITY_EDITOR
        Debug.Log(_current);
        Debug.Log(_current.transform.position);
#endif
    }
}
