using UnityEngine;

[System.Serializable]
public class LevelBoss
{
    [SerializeField] private GameObject _boss = null;

    public static event System.Action BossDied = delegate { };

    public void Init()
    {
        EnemiesInSceneCounter.LastEnemyDied += EnemiesInSceneCounter_LastEnemyDied;
    }

    private void EnemiesInSceneCounter_LastEnemyDied()
    {
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        //subbed to nomoreenemies

        if (_boss == null)
        {
            // counts as dead
            // call method
            BossDied.Invoke();
            return;
        }
    }

    public void Cleanup()
    {
        EnemiesInSceneCounter.LastEnemyDied -= EnemiesInSceneCounter_LastEnemyDied;
    }
}
