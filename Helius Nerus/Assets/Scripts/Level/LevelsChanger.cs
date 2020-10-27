using UnityEngine;

public class LevelsChanger : MonoBehaviour
{
    public static LevelsChanger Instance
    {
        get;
        private set;
    }

    private static int _currentLevel = -1;
    [SerializeField] private GameObject[] _levels = null;
    private GameObject _current = null;


    private void Awake()
    {
        Instance = this;
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        ++_currentLevel;
        if (_currentLevel >= _levels.Length)
            _currentLevel = 0;

        _current = Instantiate(_levels[_currentLevel]);

        UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(_current.gameObject, UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex((int)Scenes.INGAME));
    }

    public void ChangeLevel()
    {
        SpawnNextLevel();
    }
}
