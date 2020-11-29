using UnityEngine;
using UnityEngine.UI;

namespace HNUI
{
    public class HintCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _hintPanel = null;
        [SerializeField] private TMPro.TextMeshProUGUI _hintText = null;
        [SerializeField] private TutorialController _tutorialController = null;

        public static HintCanvas Instance
        {
            get;
            private set;
        } = null;

        private void Awake()
        {
            Instance = this;
        }

        public void SetText(string text)
        {
            _hintText.text = text;  
        }

        public void Show()
        {
            _hintPanel.SetActive(true);
        }

        public void Hide()
        {
            _hintPanel.SetActive(false);
        }

        public void ToNextStage()
        {
            // to next stage
            _tutorialController.ChangeStage();
        }
    }
}


