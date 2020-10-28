using UnityEngine;

[System.Serializable]
public class LevelBoss
{
    [SerializeField] private GameObject _boss = null;

    public static event System.Action BossDied = delegate { };
    public static event System.Action FinalBossDied = delegate { };

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
            LaunchBossDie();
            return;
        }
    }

    private void LaunchBossDie()
    {
        if (LevelsChanger.Instance.IsLastLevel)
        {
            FinalBossDied.Invoke();
        }
        else
        {
            BossDied.Invoke();
        }
    }

    public void Cleanup()
    {
        EnemiesInSceneCounter.LastEnemyDied -= EnemiesInSceneCounter_LastEnemyDied;
    }
}
