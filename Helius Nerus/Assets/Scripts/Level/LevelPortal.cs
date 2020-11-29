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
		PlayerLevelStartAnimation.Instance.EndLevelAnim();
	}

    public void Cleanup()
    {
        LevelBoss.BossDied -= LevelBoss_BossDied;
    }
}
