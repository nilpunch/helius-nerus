using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private GameObject _portalPrefab = null;
    [SerializeField] private float _portalOffset = 1.0f;

    private void Awake()
    {
        LevelBoss.BossDied += LevelBoss_BossDied;
    }

    private void LevelBoss_BossDied()
    {
        float screenHeight = Camera.main.orthographicSize;
        GameObject portal = Instantiate(_portalPrefab);
        portal.transform.parent = transform;
        portal.transform.position = new Vector3(0.0f, screenHeight - _portalOffset, 0.0f);
    }

    private void OnDestroy()
    {
        LevelBoss.BossDied -= LevelBoss_BossDied;
    }
}
