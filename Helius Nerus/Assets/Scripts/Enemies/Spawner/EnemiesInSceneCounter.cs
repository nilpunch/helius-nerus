public class EnemiesInSceneCounter
{
	public int AmountOfEnemies { get; private set; } = 0;

    public void IncrementEnemies()
    {
        ++AmountOfEnemies;
    }

    public void DectrementEnemies()
    {
        --AmountOfEnemies;
    }
} 
 