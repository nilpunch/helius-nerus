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
            //Player.Instance.IsNotMoving = true;
            //Player.Instance.IsNotShooting = true;

            //BulletPoolsContainer.Instance.ClearAllBullets();
            //ScoreCounter.Instance.Score = 0;
            //MidGameSaver.Instance.DeleteSave();
            //Simulate Die situiation
            //Player.PlayerDie
            Player.Instance.SimulateDie();

            TransitionScene.Instance.LoadUnloadScene((int)Scenes.HUB);
        }
    }
}