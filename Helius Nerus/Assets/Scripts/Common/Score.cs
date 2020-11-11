public class ScoreCounter
{
    public static event System.Action<int> ScoreWasReseted = delegate { };
    public static event System.Action ScoreWasUpdated = delegate { };

    private static int _score = 0;

    public static int Score
    {
        get => _score;
        set => _score = value;
    }

    public static void IncrementScore(int score)
    {
        _score += score;
        ScoreWasUpdated.Invoke();
    }

    public static void Reset()
    {
        ScoreWasReseted.Invoke(_score);
        _score = 0;
    }
}
