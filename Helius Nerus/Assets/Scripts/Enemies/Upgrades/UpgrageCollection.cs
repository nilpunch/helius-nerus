using UnityEngine;

public class UpgrageCollection : MonoBehaviour
{
    [SerializeField] private UpgradeBase[] _upgrades = null;

    private int _weightSum = 0;

    public static UpgrageCollection Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < _upgrades.Length; ++i)
        {
            _weightSum += _upgrades[i].Weight;
        }
    }

    public GameObject GetRandomUpgrade()
    {
        int rand = Random.Range(0, _weightSum);

        for (int i = 0; i < _upgrades.Length; ++i)
        {
            rand -= _upgrades[i].Weight;
            if (rand <= 0)
            {
                return Instantiate(_upgrades[i].gameObject);
            }
        }
        return Instantiate(_upgrades[_upgrades.Length - 1].gameObject);
    }
}
