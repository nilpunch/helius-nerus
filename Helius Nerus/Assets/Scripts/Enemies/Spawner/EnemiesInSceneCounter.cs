public class EnemiesInSceneCounter
{
    public static event System.Action LastEnemyDied = delegate { };
	public int AmountOfEnemies { get; private set; } = 0;

    private bool _spawnerFinished = false;

    public EnemiesInSceneCounter()
    {
        EnemiesSpawner.MoneyRunOut += EnemiesSpawner_MoneyRunOut;
    }

    private void EnemiesSpawner_MoneyRunOut()
    {
        _spawnerFinished = true;
        EnemiesSpawner.MoneyRunOut -= EnemiesSpawner_MoneyRunOut;
    }

    public void IncrementEnemies()
    {
        ++AmountOfEnemies;
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Enemies - " + AmountOfEnemies);
#endif
    }

    public void DectrementEnemies()
    {
        --AmountOfEnemies;
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Enemies - " + AmountOfEnemies);
#endif
        if (AmountOfEnemies == 0 && _spawnerFinished)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log("Last enemy died");
#endif
            LastEnemyDied.Invoke();
        }
    }

    ~EnemiesInSceneCounter()
    {
        if (_spawnerFinished == false)
        {
            EnemiesSpawner.MoneyRunOut -= EnemiesSpawner_MoneyRunOut;
        }
    }
} 
 