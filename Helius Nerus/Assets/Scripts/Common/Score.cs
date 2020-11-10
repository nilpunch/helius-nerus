public class ScoreCounter
{
    private static int _score = 0;

    public static int Score
    {
        get => _score;
        set => _score = value;
    }

    public static void IncrementScore(int score)
    {
        _score += score;
    }

    public static void Reset()
    {
        SaveableData.Instance.AddTotalScore(_score);
        _score = 0;
    }
}
