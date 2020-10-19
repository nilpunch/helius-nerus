using UnityEngine;

public class Level : MonoBehaviour
{
    public static EnemiesInSceneCounter EnemiesCounter
    {
        get => _instance._counter;
    }

    [SerializeField] private WallsPlacer _wallsPlacer = null;
    [SerializeField] private LevelBoss _levelBoss = null;
    [SerializeField] private LevelPortal _levelPortal = null;
    private EnemiesInSceneCounter _counter = new EnemiesInSceneCounter();
    private static Level _instance;
    private Pause _pause = new Pause();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
        _wallsPlacer.Init();
        _levelBoss.Init();
        _levelPortal.Init();
    }

    private void OnDestroy()
    {
        _levelBoss.Cleanup();
        _levelPortal.Cleanup();
    }

}
