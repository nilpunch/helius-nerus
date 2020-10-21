using System.Collections;
using UnityEngine;

[System.Serializable]
public class Pause
{
    public static event System.Action GamePaused = delegate { };
    public static event System.Action GameUnpaused = delegate { };
	public static bool Paused { get; private set; } = false;
    private static Pause _instance;

    [SerializeField] private UnityEngine.UI.Image _fadeImage = null;
    [SerializeField] private float _fadeSeconds = 1;
    [SerializeField] private float _fadeValue = 0.5f;

    public void Init()
    {
        _instance = this;
    }

    public static void PauseGame()
    {
		Paused = true;

        Color color = _instance._fadeImage.color;
        color.a = _instance._fadeValue;
        _instance._fadeImage.color = color;

        GamePaused.Invoke();
    }

    public static void UnauseGame()
    {
        CoroutineProcessor.LaunchCoroutine(_instance.FadeOut());
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

        Paused = false;

        // Launch unfade

        GameUnpaused.Invoke();
    }
}
