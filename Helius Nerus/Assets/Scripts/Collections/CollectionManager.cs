using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private UpgrageCollection _upgradeCollection = null;
    private CommandsCollection _commandsCollection = new CommandsCollection();

    [Space]
    [SerializeField] private PlayerArtifactContainer _playerArtifactContainer = null;
    [SerializeField] private WeaponModifierContainer _weaponModifierContainer = null;
    [SerializeField] private UpgradesContainer _upgradesContainer = null;


    public static CollectionManager Instance
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

        DontDestroyOnLoad(gameObject);

        _commandsCollection.Init();
        _upgradeCollection.Init();

        _playerArtifactContainer.Init();
        _weaponModifierContainer.Init();
        _upgradesContainer.Init();
    }
}
