using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [Tooltip("Поле с ключем для текста в транслите")]
    [SerializeField] private string _key = "";

    // Start is called before the first frame update
    void Start()
    {
        //Подсасываем текст компоненту
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        //И вставляем текст из локализатора
        text.text = LocalizationManager.Instance.GetLocalizedValue(_key);
    }
}
