using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class AchievementGenerationWindow : EditorWindow
{
    private string _achievementName = "AchievementClassName";
    private string _className = "ClassWithEventName";
    private string _eventName = "EventName";
    private string _achievementNameCode = "AchievementNameCode";
    private string _achievementDescCode = "AchievementDecsriptionCode";
    private string _spritePath = "SpritePath";

    [MenuItem("Window/Achievement Generator")]
    public static void ShowWindow()
    {
        GetWindow<AchievementGenerationWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Окно для создания достижений");

        _achievementName = EditorGUILayout.TextField("Название класса достижения", _achievementName);
        _className = EditorGUILayout.TextField("Название класса с событием", _className);
        _eventName = EditorGUILayout.TextField("Название события", _eventName);
        _achievementNameCode = EditorGUILayout.TextField("Код описания названия достижения для локализатора", _achievementNameCode);
        _achievementDescCode = EditorGUILayout.TextField("Код описания достижения для локализатора", _achievementDescCode);
        _spritePath = EditorGUILayout.TextField("Путь к спрайту достижения ( в ресурсах)", _spritePath);

        if (GUILayout.Button("Создать достижение"))
        {
            CreateAchievement();
        }

    }

    private void CreateAchievement()
    {
        // Проверки (не все но почти все)
        Assembly inGameAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(t  => t.GetName().Name == "Assembly-CSharp");

        if (inGameAssembly != null)
        {
            Debug.Log("Сборка загружена...");
        }
        else
        {
            Debug.LogError("Сборка не загружена! Произошла ошибка");
            return;
        }

        if (inGameAssembly.GetType(_achievementName) != null)
        {
            Debug.LogError("Достижение (класс) с таким названием уже существует!");
            return;
        }

        if (inGameAssembly.GetType(_className) == null)
        {
            Debug.LogError("Указаного класса с событием не существует!");
            return;
        }

        if (inGameAssembly.GetType(_className).GetEvent(_eventName) == null)
        {
            Debug.LogError("Указаного события в классе не существует!");
            return;
        }

        Debug.Log("Проверки пройдены. Генерация кода...");

        // Создание кода класса
        string classData =
@"public class " + _achievementName + @" : Achievment
{
    public override void Init(bool wasTriggered = false)
    {
        base.Init(wasTriggered);

        _achievementName = " +  "\"" + _achievementNameCode + "\"" + @";
        _achievementDescription = " + "\"" + _achievementDescCode + "\"" + @";
        //_sprite = Resources.Load<Sprite>(" + "\"" + _spritePath + "\"" + @");
    }

    protected override void Subscribe()
    {
        " + _className + @"." + _eventName + @" += AchievmentHappened;
    }

    protected override void Unsubscribe()
    {
        " + _className + @"." + _eventName + @" -= AchievmentHappened;
    }
}";

        Debug.Log("Код создан. Запись в файл...");

        // Пути
        string achievementFolderPath = Application.dataPath
            + "/Scripts/Achievments/";
        string achievementSystemPath = achievementFolderPath
            + "AchievementSystem.cs";

        // Запись достижения
        //File.Create(achievementFolderPath + _achievementName + ".cs");
        //File.OpenWrite(achievementFolderPath + _achievementName + ".cs");
        StreamWriter fileStream = new StreamWriter(achievementFolderPath + _achievementName + ".cs");
        fileStream.WriteLine(classData);

        fileStream.Close();

        Debug.Log("Достижение создано. Запись в систему...");

        // Запись новой ачивки в систему
        List<string> lines = new List<string>(File.ReadAllLines(achievementSystemPath));

        int index = 0;
        while (lines[index].Contains(@"/// DON'T REMOVE THIS LINE!") == false)
            ++index;
        ++index;
        lines.Insert(index, @"      _achievments.Add(new " + _achievementName + @"());");
        //lines[index] =
        //    @"      _achievments.Add(new " + _achievementName + @"());" + Environment.NewLine;

        File.WriteAllLines(achievementSystemPath, lines);

        Debug.Log("Готово!");
    }
}
