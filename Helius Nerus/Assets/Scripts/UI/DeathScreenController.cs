using UnityEngine;

namespace HNUI
{
    public class DeathScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _deathScreenGO = null;

        private void Awake()
        {
            Player.PlayerDie += Player_PlayerDie;
            _deathScreenGO.SetActive(false);
        }

        private void Player_PlayerDie()
        {
            //Pause.PauseGame(); //?
            //Player.Instance.IsStatic = true;
            //Player.Instance.IsNoShooting = true;

            // Remove all bullets!
            _deathScreenGO.SetActive(true);            

            ScoreCounter.Reset(); // here??
        }

        private void OnDestroy()
        {
            Player.PlayerDie -= Player_PlayerDie;
        }
    }
}

