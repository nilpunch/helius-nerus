using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveableData : MonoBehaviour
{
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

        // LoadData();
    }
    public void AddMaximalLevels(int amount)
    {
        // Boss has this property? 4 for all bosses, 1 for pre-last and 0 for final?
        _allSaveableFields._maximalUnlockedLevel += amount;
    }
    public void AddTotalScore(int amount)
    {
        _allSaveableFields._totalScore += amount;
    }

    private void SaveDate()
    {
        string fileName = "SaveableData.cpl";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string dataAsJson = JsonUtility.ToJson(_allSaveableFields);

        if (!string.IsNullOrEmpty(filePath))
        {
#if UNITY_EDITOR
            Debug.LogError("Saveable data can't access data path!");
#endif
            return;
        }

        if (Application.isMobilePlatform) // android and so on
        {
            // idk, should work
            File.WriteAllText(filePath, dataAsJson);
        }
        else // pc - editor in our case
        {
            File.WriteAllText(filePath, dataAsJson);
        }

    }

    private void LoadData()
    {
        string fileName = "SaveableData.cpl";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        string dataAsJson = "";
        //Android
        if (Application.platform == RuntimePlatform.Android)
        {
            //Какая-то счтука, которая может читать на андроиде файлы
            UnityWebRequest www = UnityWebRequest.Get(filePath);

            //Читаем
            www.SendWebRequest();

            //error
            if (www.isNetworkError || www.isHttpError)
            {
                return;
            }
            else
            {
                //not error
                //Не знаю зачем, написали надо
                while (!www.isDone) ;
                //А тут текст получаем
                dataAsJson = www.downloadHandler.text;
            }

        }
        //PC (for now)
        else
        {
            if (File.Exists(filePath))
            {
                //Прочитали текст
                dataAsJson = File.ReadAllText(filePath);
            }
            else
            {
#if UNITY_EDITOR
                //Файла нету
                Debug.LogError("Error - can't acces saveable data file!");
#endif
                return;
            }
        }
        if (dataAsJson == "")
        {
            _allSaveableFields = new AllSaveableFields();
        }
        else
        {
            _allSaveableFields = JsonUtility.FromJson<AllSaveableFields>(dataAsJson);
        }
    }
}

[System.Serializable]
public class AllSaveableFields
{
    public int _amountOfMoney = 0;
    public List<int> _unlockedShips = new List<int>();
    public List<int> _boughtShips = new List<int>();
    public int _maximalScore = 0;
    public int _totalScore = 0;
    public List<int> _unlockedArtifacts = new List<int>();
    public int _maximalUnlockedLevel = 0;
    public List<int> _openedWeaponModifiers = new List<int>();
    public int _donatedToMonument = 0;
}