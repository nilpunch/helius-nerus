using UnityEngine;

public class EnemiesInSceneCounter
{
	public static int AmountOfEnemies { get; private set; } = 0;

    public static void IncrementEnemies()
    {
        ++AmountOfEnemies;
    }

    public static void DectrementEnemies()
    {
        --AmountOfEnemies;
    }
}
