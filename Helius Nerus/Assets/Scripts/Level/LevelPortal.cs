using UnityEngine;

[System.Serializable]
public class LevelPortal
{
    [SerializeField] private GameObject _portalPrefab = null;
    [SerializeField] private float _portalOffset = 1.0f;
    [SerializeField] private Transform _transform = null;

    public void Init()
    {
        LevelBoss.BossDied += LevelBoss_BossDied;
    }

    private void LevelBoss_BossDied()
    {
        float screenHeight = ParallaxCamera.ParallaxSize.y / 2;
        GameObject portal = GameObject.Instantiate(_portalPrefab);
        portal.transform.parent = _transform;
        portal.transform.position = new Vector3(0.0f, screenHeight - _portalOffset, 0.0f);
    }

    public void Cleanup()
    {
        LevelBoss.BossDied -= LevelBoss_BossDied;
    }
}
