using System.Collections;
using UnityEngine;

[System.Serializable]
public class Pause
{
    public static event System.Action GamePaused = delegate { };
    /// <summary>
    /// Game WAS unpaused
    /// </summary>
    public static event System.Action GameUnpaused = delegate { };
    /// <summary>
    /// Game started to unpause
    /// </summary>
    public static event System.Action GameUnpausing = delegate { };

    private bool _paused = false;

    public static Pause Instance
    {
        get;
        private set;
    } = null;
    public static float UnpauseDuration
    {
        get => 3.0f;
    }

    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new Pause();
        }
    }

    public static void PauseGame()
    {
        TimeManager.PauseAll();

        Instance._paused = true;

        GamePaused.Invoke();
    }

    public static void UnpauseGame()
    {
        GameUnpausing.Invoke();
        CoroutineProcessor.LaunchCoroutine(Instance.UnpauseCounter());
    }

    private IEnumerator UnpauseCounter()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed <= UnpauseDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        TimeManager.UnpauseAll();

        _paused = false;

        GameUnpaused.Invoke();
    }

    public static void TogglePause()
    {
        if (Instance._paused)
            UnpauseGame();
        else
            PauseGame();
    }
}
