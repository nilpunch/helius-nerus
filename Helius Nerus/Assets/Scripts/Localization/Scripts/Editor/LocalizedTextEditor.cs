using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    private Vector2 _scrollViewPos;
    public LocalizationData LocalizationData;

    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
    }

    private void OnGUI()
    {
        _scrollViewPos = EditorGUILayout.BeginScrollView(_scrollViewPos, GUILayout.ExpandHeight(true));
        if (LocalizationData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("LocalizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            //Это создание кнопки и коллбэк ее нажатия
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        //и еще два раза
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Create new data"))
        {
            CreateGameData();
        }

        EditorGUILayout.EndScrollView();
    }
    
    //Создание нового объекта
    private void CreateGameData()
    {
        LocalizationData = new LocalizationData();
    }

    //Сохранение через окошки от винды. Удобно
    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            //Конвертируем в джсон
            string dataAsJson = JsonUtility.ToJson(LocalizationData);
            //Записываем в файл
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void LoadGameData()
    {
        //Обратно окошко проводника винды
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");
        if (!string.IsNullOrEmpty(filePath))
        {
            //Считываем текст
            string dataAsJson = File.ReadAllText(filePath);
            //И деконвертируем джсон
            LocalizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }
}
