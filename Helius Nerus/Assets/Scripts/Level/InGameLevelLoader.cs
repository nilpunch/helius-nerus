using UnityEngine;

public class InGameLevelLoader : MonoBehaviour
{
    private GameObject _newLevel;

    private void Awake()
    {
        // temp
        int currentLevel = 0;

        _newLevel = Instantiate(Resources.Load<GameObject>("Levels/Level_" + currentLevel));
    }

    private void Update()
    {
        _newLevel.transform.parent = null;

        Destroy(gameObject);
    }
}
