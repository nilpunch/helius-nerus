using UnityEngine;

namespace HNUI
{
    public class WinScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreenGO = null;

        private void Awake()
        {
            LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
            _winScreenGO.SetActive(false);
        }

        private void LevelBoss_FinalBossDied()
        {
            //Pause.PauseGame(); //?
            Player.Instance.IsStatic = true;
            Player.Instance.IsNoShooting = true;

            BulletPoolsContainer.Instance.ClearAllBullets();

            _winScreenGO.SetActive(true);

            // Temporary
            SaveableData.Instance.AddMaximalLevels(1);

            ScoreCounter.Reset(); // here??
        }

        private void OnDestroy()
        {
            LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
        }
    }
}

