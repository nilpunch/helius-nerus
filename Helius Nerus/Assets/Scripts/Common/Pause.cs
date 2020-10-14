public class Pause
{
    public static event System.Action GamePaused = delegate { };
    public static event System.Action GameUnpaused = delegate { };

	public static bool Paused { get; private set; } = false;

    static public void PauseGame()
    {
		Paused = true;
		GamePaused.Invoke();
    }

    static public void UnauseGame()
    {
		Paused = false;
		GameUnpaused.Invoke();
    }
}
