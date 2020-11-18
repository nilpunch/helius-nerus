using UnityEngine;

namespace HNUI
{
    public class WinScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreenGO = null;

        private void OnEnable()
        {
            LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
            _winScreenGO.SetActive(false);
        }

        private void LevelBoss_FinalBossDied(int a)
        {
            //Pause.PauseGame(); //?
            Player.Instance.IsNotMoving = true;
            Player.Instance.IsNotShooting = true;

            BulletPoolsContainer.Instance.ClearAllBullets();

            _winScreenGO.SetActive(true);

        }

        private void OnDisable()
        {
            LevelBoss.FinalBossDied -= LevelBoss_FinalBossDied;
        }
    }
}

