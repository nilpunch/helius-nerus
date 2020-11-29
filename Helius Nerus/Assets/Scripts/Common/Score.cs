using System;
using UnityEngine;

public class ScoreCounter
{
    public static event System.Action<int> ScoreWasReseted = delegate { };
    public static event System.Action ScoreWasUpdated = delegate { };

    private int _score = 0;

    public static ScoreCounter Instance
    {
        get;
        private set;
    } = null;
    public int Score
    {
        get => Instance._score;
        set => Instance._score = value;
    }

    public ScoreCounter()
    {
        Player.PlayerDie += Player_PlayerDie;
        LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
        Enemy.EnemyDie += Enemy_EnemyDie;
    }

    private void LevelBoss_FinalBossDied(int obj)
    {
        Reset();
    }

    private void Player_PlayerDie()
    {
        Reset();
    }

    private void Enemy_EnemyDie(int obj)
    {
        IncrementScore(obj);
    }

    private void Unsubscribe()
    {
        Player.PlayerDie -= Player_PlayerDie;
        LevelBoss.FinalBossDied -= LevelBoss_FinalBossDied;
        Enemy.EnemyDie -= Enemy_EnemyDie;
    }

    public static void Initialize()
    {
        if (Instance == null)
            Instance = new ScoreCounter();
    }

    public static void Cleanup()
    {
        if (Instance != null)
            Instance.Unsubscribe();
    }

    public void IncrementScore(int score)
    {
        _score += score;
        ScoreWasUpdated.Invoke();
    }

    public void Reset()
    {
        ScoreWasReseted.Invoke(_score);
        _score = 0;
    }
}
