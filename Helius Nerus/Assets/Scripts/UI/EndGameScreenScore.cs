using UnityEngine;

namespace HNUI
{
    public class EndGameScreenScore : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text = null;

        private void OnEnable()
        {
            _text.text = LocalizationManager.Instance.GetLocalizedValue("scoredCode") + ScoreCounter.Instance.Score;
        }
    }
}