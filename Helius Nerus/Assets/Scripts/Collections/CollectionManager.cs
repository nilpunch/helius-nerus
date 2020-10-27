using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private UpgrageCollection _upgradeCollection = null;
    //[SerializeField] private EndLevelUpgradeCollection _endLevelUpgradeCollection = null;
    //private ArtifactsCollection _artifactsCollection = new ArtifactsCollection();
    private CommandsCollection _commandsCollection = new CommandsCollection();
    //private ModifiersCollection _modifiersCollection = new ModifiersCollection();

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

        //_artifactsCollection.Init();
        _commandsCollection.Init();
        //_modifiersCollection.Init();
        _upgradeCollection.Init();
        //_endLevelUpgradeCollection.Init();

        _playerArtifactContainer.Init();
        _weaponModifierContainer.Init();
        _upgradesContainer.Init();
    }
}
