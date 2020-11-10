using UnityEngine;

namespace HNUI
{
    public class ResumeGameButtonScript : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button _button = null;

        private void OnEnable()
        {
            if (MidGameSaver.MidGameSaveExists == false)
                _button.interactable = false;
            else
                _button.interactable = true;
        }

        public void ResumeGame()
        {
            MidGameSaver.LoadGame();
        }
    }
}


