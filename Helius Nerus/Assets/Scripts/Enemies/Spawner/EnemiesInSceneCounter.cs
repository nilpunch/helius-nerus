public class EnemiesInSceneCounter
{
    public static event System.Action LastEnemyDied = delegate { };
	public static event System.Action EnemyCountChanged = delegate { };

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
		EnemyCountChanged.Invoke();
	}

    public void DectrementEnemies()
    {
        --AmountOfEnemies;
		EnemyCountChanged.Invoke();
		if (AmountOfEnemies == 0 && _spawnerFinished)
        {
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