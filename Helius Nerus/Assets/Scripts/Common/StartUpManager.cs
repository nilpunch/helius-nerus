using UnityEngine;

public class StartUpManager : MonoBehaviour
{
    private void Awake()
    {
        MidGameSaver.Initialize();
        ScoreCounter.Initialize();

        Destroy(gameObject);
    }
}