using UnityEngine;

namespace HNUI
{
    public class ScoreUpdater : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText = null;

        private void OnEnable()
        {
            ScoreCounter.ScoreWasUpdated += ScoreCounter_ScoreWasUpdated;
        }

        private void ScoreCounter_ScoreWasUpdated()
        {
            _scoreText.text = ScoreCounter.Instance.Score.ToString();
        }

        private void OnDisable()
        {
            ScoreCounter.ScoreWasUpdated -= ScoreCounter_ScoreWasUpdated;
        }
    }
}

