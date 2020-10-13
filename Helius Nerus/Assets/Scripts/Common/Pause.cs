public class Pause
{
    public static event System.Action GamePaused = delegate { };
    public static event System.Action GameUnpaused = delegate { };


    public void PauseGame()
    {
        GamePaused.Invoke();
    }

    public void UnauseGame()
    { 
        GameUnpaused.Invoke();
    }
}
