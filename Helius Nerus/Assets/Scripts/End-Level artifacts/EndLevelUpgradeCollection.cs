using UnityEngine;

[System.Serializable]
public class EndLevelUpgradeCollection
{
    [SerializeField] private PlayerWeaponsParametrs[] _upgrades = null;

    public static EndLevelUpgradeCollection Instance
    {
        get;
        private set;
    }

    public void Init()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            return;
        }
    }

    public PlayerWeaponsParametrs GetRandomUpgrade()
    {
        int rand = Random.Range(0, _upgrades.Length);

        return _upgrades[rand].Clone();
    }
}
