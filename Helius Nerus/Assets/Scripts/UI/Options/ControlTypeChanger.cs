using UnityEngine;

namespace HNUI
{
    public class ControlTypeChanger : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text = null;
    
        [SerializeField] private TMPro.TextMeshProUGUI[] _controlTexts = null;

        private void Awake()
        {
            ChangeControlType(0);
        }

        public void ChangeControlType(int num)
        {
            for (int i = 0; i < _controlTexts.Length; ++i)
            {
                _controlTexts[i].color = Color.gray;
            }
            _controlTexts[num].color = Color.white;
            PlayerPrefs.SetString("ControlType", _controlTexts[num].text);

            _text.text = LocalizationManager.Instance.GetLocalizedValue("ControlType") + _controlTexts[num].text;
        }
    }
}



