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
    private EnemiesInSceneCounter _counter = new EnemiesInSceneCounter();
    private static Level _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

}
