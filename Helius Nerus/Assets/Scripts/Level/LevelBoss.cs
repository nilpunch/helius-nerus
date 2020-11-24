using UnityEngine;

[System.Serializable]
public class LevelBoss
{
    public static event System.Action BossDied = delegate { };
    public static event System.Action<int> FinalBossDied = delegate { };

    [SerializeField] private GameObject _boss = null;

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
            LaunchBossDie(0);
            return;
        }

        // spawn boss
        GameObject boss = GameObject.Instantiate(_boss);
        // subscribe to boss die
        Boss.BossDied += Boss_BossDied;
    }

    private void Boss_BossDied(int obj)
    {
        LaunchBossDie(obj);
    }

    private void LaunchBossDie(int amn)
    {
        Boss.BossDied -= Boss_BossDied;

        if (LevelsChanger.Instance.IsLastLevel)
        {
            // get levels counter from last boss
            FinalBossDied.Invoke(amn);
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
