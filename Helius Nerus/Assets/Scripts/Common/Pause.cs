using System.Collections;
using UnityEngine;

[System.Serializable]
public class Pause
{
    public static event System.Action GamePaused = delegate { };
    public static event System.Action GameUnpaused = delegate { };
    private static Pause _instance;

    [SerializeField] private UnityEngine.UI.Image _fadeImage = null;
    [SerializeField] private float _fadeSeconds = 1;
    [SerializeField] private float _fadeValue = 0.5f;

    private static bool _paused = false;

    public void Init()
    {
        _instance = this;
    }

    public static void PauseGame()
    {
        TimeManager.PauseAll();

        _paused = true;

        Color color = _instance._fadeImage.color;
        color.a = _instance._fadeValue;
        _instance._fadeImage.color = color;

        GamePaused.Invoke();
    }

    public static void UnpauseGame()
    {
        CoroutineProcessor.LaunchCoroutine(_instance.FadeOut());
    }

    public static void TogglePause()
    {
        if (_paused)
            UnpauseGame();
        else
            PauseGame();
    }

    private IEnumerator FadeOut()
    {
        float timeElapsed = 0.0f;
        float fadePerSeconds = (0 - _fadeImage.color.a) / _fadeSeconds;

        Color color;

        while (timeElapsed <= _fadeSeconds)
        {
            timeElapsed += Time.deltaTime;

            color = _fadeImage.color;
            color.a += fadePerSeconds * Time.deltaTime;
            _fadeImage.color = color;
            yield return null;
        }

        TimeManager.UnauseAll();

        _paused = false;

        // Launch unfade

        GameUnpaused.Invoke();
    }
}
