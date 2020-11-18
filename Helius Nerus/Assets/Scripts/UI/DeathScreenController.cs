using UnityEngine;

namespace HNUI
{
    public class DeathScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _deathScreenGO = null;

        private void OnEnable()
        {
            Player.PlayerDie += Player_PlayerDie;
            _deathScreenGO.SetActive(false);
        }

        private void Player_PlayerDie()
        {
            BulletPoolsContainer.Instance.ClearAllBullets();

            // Pause game
            //Pause.PauseGame();

            _deathScreenGO.SetActive(true);            
        }

        private void OnDisable()
        {
            Player.PlayerDie -= Player_PlayerDie;
        }
    }
}

