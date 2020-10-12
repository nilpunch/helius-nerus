using UnityEngine;

public class LevelBoss : MonoBehaviour
{
    [SerializeField] private GameObject _boss = null;

    public static event System.Action BossDied = delegate { };

    private void Awake()
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
#if UNITY_EDITOR
            Debug.Log("Boss died");
#endif
            BossDied.Invoke();
            return;
        }
    }

    private void OnDestroy()
    {
        EnemiesInSceneCounter.LastEnemyDied -= EnemiesInSceneCounter_LastEnemyDied;
    }
}
