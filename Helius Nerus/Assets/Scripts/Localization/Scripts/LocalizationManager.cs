using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class LocalizationManager : MonoBehaviour
{
    //Синглтон
    private static LocalizationManager _instance;

    //Словарик с ключем-переводом
    private Dictionary<string, string> _localizedText;
    //Если текст не найдет
    private string _missingTextString = "Missing text in translation: ";
    //Датчик готовности
    private bool _isReady = false;

    public static LocalizationManager Instance => _instance;
    public bool IsReady => _isReady;

    //Синглтон
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        //Он понадобится во всех сценах
        DontDestroyOnLoad(gameObject);

        LoadLocalizedText(Application.systemLanguage + ".json");
    }

    //Грузим перевод из json-файла
    public void LoadLocalizedText(string fileName)
    {
        //Создали словарь
        _localizedText = new Dictionary<string, string>();
        //Тут валяется файл
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

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
                //here we load default
                www = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "English.json"));
                www.SendWebRequest();
                while (!www.isDone) ;
                dataAsJson = www.downloadHandler.text;
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
                Debug.LogWarning("Warning - localization file with name " + fileName + " not exists! Loading English");
                #endif
                LoadLocalizedText("English.json");
                return;
            }
        }
        //Десериализовали (это какая-то встроенная конверсия, не чипай ее или пиши свою
        LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        for (int i = 0; i < localizationData.Items.Length; i++)
        {
            //Впихнули элементы массива в словарь, потому что словарь не сериализуется
            _localizedText.Add(localizationData.Items[i].Key, localizationData.Items[i].Value);
        }

        #if UNITY_EDITOR
        //Для порядка
        Debug.Log("Data loaded, dictionary contains " + _localizedText.Count + " items");
        #endif
        //Закончили операцию
        _isReady = true;
    }

    //Получаем текст
    public string GetLocalizedValue(string key)
    {
        if (_localizedText.ContainsKey(key))
        {
            return _localizedText[key];
        }
        //Если текста нет, варнингуем для понятия, какой ключ протерялся
#if UNITY_EDITOR
        Debug.LogWarning(_missingTextString + key);
#endif
        return _missingTextString + key;
    }
}
