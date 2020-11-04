using UnityEngine;

public class Level : MonoBehaviour
{
    private static Level _instance;

    [SerializeField] private WallsPlacer _wallsPlacer = null;
    [SerializeField] private LevelBoss _levelBoss = null;
    [SerializeField] private LevelPortal _levelPortal = null;
    [SerializeField] private Pause _pause = null;

    private EnemiesInSceneCounter _counter = new EnemiesInSceneCounter();

    public static EnemiesInSceneCounter EnemiesCounter
    {
        get => _instance._counter;
    }

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
        _pause.Init();
    }

    private void OnDestroy()
    {
        _levelBoss.Cleanup();
        _levelPortal.Cleanup();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Pause.Paused == false)
            {
                Pause.PauseGame();
            }
            else
            {
                Pause.UnauseGame();
            }
        }
    }
#endif
}
