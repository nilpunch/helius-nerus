using UnityEngine;
using UnityEngine.EventSystems;

namespace HNUI
{
    public class SoundTypeUIElement : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("Название типа звука, также код в локализаторе")]
        [SerializeField] private TMPro.TextMeshProUGUI _text = null;
        [SerializeField] private string _volumeType = "SFX";
        [Header("TMPro color tags!")]
        [Header("Supported color names are black, blue, green, orange, purple, red, white, and yellow.")]
        [SerializeField] private string _onColor = "green";
        [SerializeField] private string _offColor = "red";

        private string _onText;
        private string _offText;

        private bool _toggled = false;

        private void Awake()
        {
            _onText = LocalizationManager.Instance.GetLocalizedValue("on");
            _offText = LocalizationManager.Instance.GetLocalizedValue("off");

            Toggle();
        }

        public void Toggle()
        {
            _toggled = !_toggled;
            if (_toggled)
            {
                SetText(LocalizationManager.Instance.GetLocalizedValue(_volumeType), _onColor, _onText);
                PlayerPrefs.SetFloat(_volumeType, 1.0f);
            }
            else
            {
                SetText(LocalizationManager.Instance.GetLocalizedValue(_volumeType), _offColor, _offText);
                PlayerPrefs.SetFloat(_volumeType, 0.0f);
            }
        }

        private void SetText(string volumeType, string color, string text)
        {
            _text.text = "<color=\"white\">" + volumeType + " : <color=\"" + color + "\">" + text;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Toggle();
        }
    }
}



