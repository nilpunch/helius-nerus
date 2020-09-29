using UnityEngine;

public class Game_Temp : MonoBehaviour
{
    public static Game_Temp Instance
    {
        get => _instance;
    }
    public EnemiesInSceneCounter EnemiesCounter
    {
        get => _counter;
    }
    private EnemiesInSceneCounter _counter = new EnemiesInSceneCounter();
    private static Game_Temp _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

}
