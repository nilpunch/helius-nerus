using UnityEngine;

public class EnemiesInSceneCounter : MonoBehaviour
{
    private static int _amountOfEnemies;
    public static int AmountOfEnemies => _amountOfEnemies;

    //А он тут нужен?
    private static EnemiesInSceneCounter _instance;
    public static EnemiesInSceneCounter Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _amountOfEnemies = 0;
    }

    public static void IncrementEnemies()
    {
        ++_amountOfEnemies;
    }

    public static void DectrementEnemies()
    {
        --_amountOfEnemies;
    }
}
