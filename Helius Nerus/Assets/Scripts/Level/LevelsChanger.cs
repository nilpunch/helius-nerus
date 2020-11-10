using UnityEngine;

public class LevelsChanger : MonoBehaviour
{
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
        get => CurrentLevel == _maximalUnlockedLevel;
    }
    public static int CurrentLevel
    {
        get;
        set;
    } = -1;

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
        CurrentLevel = -1;
    }

    private void SpawnNextLevel()
    {
        ++CurrentLevel;
        if (CurrentLevel >= _levels.Length)
            CurrentLevel = 0;

        _current = Instantiate(_levels[CurrentLevel]);

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
