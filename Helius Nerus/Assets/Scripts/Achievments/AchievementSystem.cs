using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance
    {
        get => _instance;
    }

    [SerializeField] private Achievment[] _achievments = null;

    private static AchievementSystem _instance = null;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        LoadAchievements();

        foreach (Achievment achievment in _achievments)
        {
            achievment.Init();
        }
    }

    private void SaveAchievments()
    {
        return;
    }

    private void LoadAchievements()
    {
        return;
    }

}
