using UnityEngine;

public class SaveableData : MonoBehaviour
{
    public static SaveableData Instance
    {
        get;
        private set;
    } = null;

    public int MaximalUnlockedLevel
    {
        get => _maximalUnlockedLevel;
    }
    private ScoreCounter _score = new ScoreCounter();
    private int _maximalUnlockedLevel = 0;
    private int _totalScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // LoadData();
    }
    public void AddMaximalLevels(int amount)
    {
        // Boss has this property? 4 for all bosses, 1 for pre-last and 0 for final?
        _maximalUnlockedLevel += amount;
    }
    public void AddTotalScore(int amount)
    {
        _totalScore += amount;
    }

    private void SaveDate()
    {
        // write
    }

    private void LoadData()
    {
        // read assing
    }
}
