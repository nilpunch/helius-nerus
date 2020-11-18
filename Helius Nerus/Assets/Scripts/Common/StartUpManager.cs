using UnityEngine;

public class StartUpManager : MonoBehaviour
{
    private System.Collections.IEnumerator Start()
    {
        MidGameSaver.Initialize();
        ScoreCounter.Initialize();
        AchievementSystem.Initialize();

        Destroy(gameObject);
        yield break;
    }
}