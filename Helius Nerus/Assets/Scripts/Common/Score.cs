public class ScoreCounter
{
    public static int Score
    {
        get => _score;
    }
    private static int _score = 0;

    public static void IncrementScore(int score)
    {
        _score += score;
    }

    public static void Reset()
    {
        SaveableData.Instance.AddTotalScore(_score / 100); // here?
        _score = 0;
    }

}
