using UnityEngine;

namespace HNUI
{
    public class ScoreUpdater : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText = null;

        // Update is called once per frame
        void Update()
        {
            _scoreText.text = ScoreCounter.Score.ToString();
        }
    }
}

