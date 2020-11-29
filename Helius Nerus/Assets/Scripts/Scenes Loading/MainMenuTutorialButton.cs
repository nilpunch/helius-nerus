using UnityEngine;

namespace HNUI
{
    public class MainMenuTutorialButton : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text = null;
        [SerializeField] private string _toHubText = "";
        [SerializeField] private string _tutorialText = "";

        private Scenes _scenesToLoad = Scenes.INGAME;

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey("Tutorial") == false)
            {
                ChangeButton(Scenes.TUTORIAL, _tutorialText);
            }
            else
            {
                ChangeButton(Scenes.HUB, _toHubText);
            }
        }

        private void ChangeButton(Scenes scene, string text)
        {
            _scenesToLoad = scene;
            _text.text = LocalizationManager.Instance.GetLocalizedValue(text);
        }

        public void OnClick()
        {
            TransitionScene.Instance.LoadUnloadScene((int)_scenesToLoad);
        }
    }
}