using UnityEngine;
using DG.Tweening;

public class StartUpManager : MonoBehaviour
{
    private System.Collections.IEnumerator Start()
    {
        MidGameSaver.Initialize();
        ScoreCounter.Initialize();
        AchievementSystem.Initialize();

        DOTween.Init();

        Destroy(gameObject);
        yield break;
    }
}