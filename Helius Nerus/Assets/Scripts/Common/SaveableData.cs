using System.Collections.Generic;
using UnityEngine;

public class SaveableData : MonoBehaviour
{
    // ALL DATA MODIFICATIONS SHOULD CALL SAVEDATA

    [System.Serializable]
    private class AllSaveableFields
    {
        public int _amountOfMoney = 0;
        public int _maximalScore = 0;
        public int _totalScore = 0;
        public int _donatedToMonument = 0;
        public int _maximalUnlockedLevel = 0;
        public List<int> _unlockedArtifacts = new List<int>();
        public List<int> _unlockedShips = new List<int>();
        public List<int> _boughtShips = new List<int>();
        public List<int> _openedWeaponModifiers = new List<int>();

        public void SetDefault()
        {
            _amountOfMoney = 0;
            _unlockedShips = new List<int>();
            _boughtShips = new List<int>();
            _maximalScore = 0;
            _totalScore = 0;
            _unlockedArtifacts = new List<int>();
            _maximalUnlockedLevel = 0;
            _openedWeaponModifiers = new List<int>();
            _donatedToMonument = 0;
        }
    }

    [Tooltip("Число, на которое делится счет для перевода в деньги")]
    [SerializeField] private int _scoreDenominator = 100;

    private ScoreCounter _score = new ScoreCounter();
    private AllSaveableFields _allSaveableFields = new AllSaveableFields();

    public static SaveableData Instance
    {
        get;
        private set;
    } = null;

    public int MaximalUnlockedLevel
    {
        get => _allSaveableFields._maximalUnlockedLevel;
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

        LoadData();
    }


    public void AddMaximalLevels(int amount)
    {
        // Boss has this property? 4 for all bosses, 1 for pre-last and 0 for final?
        _allSaveableFields._maximalUnlockedLevel += amount;
        SaveData();
    }

    /// <summary>
    /// Adds total score, also checks for maximal score and money
    /// </summary>
    /// <param name="amount">Количество очков полученных</param>
    public void AddTotalScore(int amount)
    {
        _allSaveableFields._totalScore += amount;
        _allSaveableFields._amountOfMoney += amount / _scoreDenominator;
        if (_allSaveableFields._maximalScore < amount)
            _allSaveableFields._maximalScore = amount;

        SaveData();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(JsonUtility.ToJson(_allSaveableFields));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddMaximalLevels(1);
            AddTotalScore(1);
            _allSaveableFields._unlockedShips.Add(1);
        }
    }
#endif

    private void ResetData()
    {
        _allSaveableFields.SetDefault();
        SaveData();
    }

    private void SaveData()
    {
        //      From documemtation:
        // If the JSON representation is missing any fields, they will be given their default values 
        // (i.e. a field of type T will have value default(T) - it will not be given any value specified as a field initializer
        // , as the constructor for the object is not executed during deserialization).
        PlayerPrefs.SetString("SaveableData", JsonUtility.ToJson(_allSaveableFields));
        
        // Нужно ли это прямо здесь или вынести куда-то типа закрытия приложения? Насколько сильно оно жрет

        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("SaveableData") == false)
        {
#if UNITY_EDITOR
            Debug.Log("PlayerPrefs for SaveableData not exist!");
#endif
            _allSaveableFields = new AllSaveableFields();
            _allSaveableFields.SetDefault();
            SaveData();
            return;
        }
        _allSaveableFields = JsonUtility.FromJson<AllSaveableFields>(PlayerPrefs.GetString("SaveableData"));
    }
}
