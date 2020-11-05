using UnityEngine;

public static class TimeManager
{
    private static float _enemyTimeScale = 1.0f;
    private static float _playerTimeScale = 1.0f;
    private static float _animationTimeScale = 1.0f;
    private static float _uiTimeScale = 1.0f;

    /// <summary>
    /// Враги, Пули врагов, Спаунер
    /// </summary>
    public static float EnemyDeltaTime
    {
        get => _enemyTimeScale * Time.deltaTime;
        set
        {
            if (value < 0)
                _enemyTimeScale = 0;
            else
                _enemyTimeScale = value;
        }
    }

    /// <summary>
    /// Движение игрока, стрельба игрока, пули и модификаторы игрока
    /// </summary>
    public static float PlayerDeltaTime
    {
        get => _playerTimeScale * Time.deltaTime;
        set
        {
            if (value < 0)
                _playerTimeScale = 0;
            else
                _playerTimeScale = value;
        }
    }

    /// <summary>
    /// Анимация фона, начала и конца уровня
    /// </summary>
    public static float AnimationDeltaTime
    {
        get => _animationTimeScale * Time.deltaTime;
        set
        {
            if (value < 0)
                _animationTimeScale = 0;
            else
                _animationTimeScale = value;
        }
    }

    public static float UIDeltaTime
    {
        get => _uiTimeScale * Time.deltaTime;
        set
        {
            if (value < 0)
                _uiTimeScale = 0;
            else
                _uiTimeScale = value;
        }
    }

    public static void PauseAll()
    {
        _enemyTimeScale = 0;
        _playerTimeScale = 0;
        _animationTimeScale = 0;
        _uiTimeScale = 0;
    }

    public static void UnauseAll()
    {
        _enemyTimeScale = 1.0f;
        _playerTimeScale = 1.0f;
        _animationTimeScale = 1.0f;
        _uiTimeScale = 1.0f;
    }
}