using UnityEngine;

public class EnemiesInSceneCounter : MonoBehaviour
{
    //А он тут нужен?
    public static EnemiesInSceneCounter Instance => _instance;
    private static EnemiesInSceneCounter _instance;

    public static int AmountOfEnemies => _amountOfEnemies;
    private static int _amountOfEnemies;

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
