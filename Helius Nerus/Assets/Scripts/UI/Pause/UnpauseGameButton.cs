using UnityEngine;

namespace HNUI
{
    public class UnpauseGameButton : MonoBehaviour
    {
        public void UnpauseGame()
        {
            Pause.TogglePause();
        }

        public void ReturnToHub()
        {
            Player.Instance.SimulateDie();

            TransitionScene.Instance.LoadUnloadScene((int)Scenes.HUB);
        }
    }
}