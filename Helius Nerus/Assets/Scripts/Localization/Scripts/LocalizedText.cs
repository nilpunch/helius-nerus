using UnityEngine;
using TMPro;

namespace HNUI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [Tooltip("Вводить код локализатора прямо в текст")]
        [SerializeField] private TextMeshProUGUI _text = null;
        [Tooltip("Альтернативно ввести код подсказки сюда")]
        [SerializeField] private string _key = "";

        // Start is called before the first frame update
        void Awake()
        {
            if (_key != null && _key != "")
            {
                _text.text = LocalizationManager.Instance.GetLocalizedValue(_key);
            }
            else
            {
                _text.text = LocalizationManager.Instance.GetLocalizedValue(_text.text);
            }

            Destroy(this);
        }
    }
}

