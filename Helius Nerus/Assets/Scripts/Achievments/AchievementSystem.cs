using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    private static AchievementSystem _instance = null;

    private List<Achievment> _achievments = new List<Achievment>();

    public static AchievementSystem Instance
    {
        get => _instance;
    }

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

        AddAchievementsHere();

        LoadAchievements();

        foreach (Achievment achievment in _achievments)
        {
            achievment.Init();
        }
    }

    private void AddAchievementsHere()
    {
        _achievments.Add(new DeathAchievement());
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
