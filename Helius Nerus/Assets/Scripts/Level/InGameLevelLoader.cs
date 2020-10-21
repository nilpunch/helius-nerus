using UnityEngine;

public class InGameLevelLoader : MonoBehaviour
{
    private void Awake()
    {
        // temp
        int currentLevel = 0;

        Resources.Load<GameObject>("Levels/Level_" + currentLevel);

        Destroy(gameObject);
    }
}
