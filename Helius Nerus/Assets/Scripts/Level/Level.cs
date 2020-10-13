using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance
    {
        get => _instance;
    }
    public EnemiesInSceneCounter EnemiesCounter
    {
        get => _counter;
    }
    public Pause Pause
    {
        get => _pause;
    }

    private EnemiesInSceneCounter _counter = new EnemiesInSceneCounter();
    private static Level _instance;
    private Pause _pause = new Pause();

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance.gameObject);
        _instance = this;
    }

}
