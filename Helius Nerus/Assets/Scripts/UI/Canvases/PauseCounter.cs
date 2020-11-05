using System.Collections;
using UnityEngine;

namespace HNUI
{
    public class PauseCounter : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text = null;

        private void OnEnable()
        {
            Pause.GameUnpausing += Pause_GameUnpausing;
        }

        private void Pause_GameUnpausing()
        {
            StartCoroutine(CounterAnimation());
        }

        private IEnumerator CounterAnimation()
        {
            float timeElapsed = 0.0f;
            _text.gameObject.SetActive(true);
            while (timeElapsed <= 3.0f)
            {
                // АААА Я не понимаю реально в чем дело
                timeElapsed += Time.deltaTime;
                _text.text = Mathf.Ceil(3.0f - timeElapsed).ToString();
                yield return null;
            }
            _text.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Pause.GameUnpausing -= Pause_GameUnpausing;
        }
    }
}