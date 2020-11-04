using UnityEngine;

public class LevelsChanger : MonoBehaviour
{

    private static int _currentLevel = -1;

    [SerializeField] private GameObject[] _levels = null;

    private int _maximalUnlockedLevel = 0;
    private GameObject _current = null;

    public static LevelsChanger Instance
    {
        get;
        private set;
    }
    public bool IsLastLevel
    {
        get => _currentLevel == _maximalUnlockedLevel;
    }

    private void Awake()
    {
        Instance = this;
        SpawnNextLevel();
        _maximalUnlockedLevel = SaveableData.Instance.MaximalUnlockedLevel;

        Player.PlayerDie += Player_PlayerDie;
        LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
    }

    private void LevelBoss_FinalBossDied()
    {
        Reset();
    }

    private void Player_PlayerDie()
    {
        Reset();
    }

    private void Reset()
    {
        _currentLevel = -1;
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

    private void OnDestroy()
    {
        Player.PlayerDie -= Player_PlayerDie;
        LevelBoss.FinalBossDied -= LevelBoss_FinalBossDied;
    }
}
