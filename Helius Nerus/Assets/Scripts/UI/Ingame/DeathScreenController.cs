using UnityEngine;

namespace HNUI
{
    public class DeathScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject _deathScreenGO = null;

        private void OnEnable()
        {
            Player.PlayerDie += Player_PlayerDie;
            TransitionScene.NewSceneWasLoaded += TransitionScene_NewSceneWasLoaded;
            _deathScreenGO.SetActive(false);
        }

        private void TransitionScene_NewSceneWasLoaded(Scenes obj)
        {
            _deathScreenGO.SetActive(false);
        }

        private void Player_PlayerDie()
        { 

            _deathScreenGO.SetActive(true);            
        }

        private void OnDisable()
        {
            Player.PlayerDie -= Player_PlayerDie;
            TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
        }
    }
}

