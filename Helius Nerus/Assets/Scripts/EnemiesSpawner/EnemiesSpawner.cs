using UnityEngine;

[RequireComponent(typeof(EnemiseSpawnerControlledRandom))]
public class EnemiesSpawner : MonoBehaviour
{
    [Tooltip("СО с пачками врагов")]
    [SerializeField] private StackOfEnemies[] _enemies = null;
    [Tooltip("Начальное количество денег")]
    [SerializeField] private int _money = 10;
    [Tooltip("Доход в секунду")]
    [SerializeField] private int _moneyPerSecond = 10;
    [Tooltip("Сумма трат за уровнень")]
    [SerializeField] private int _levelSpendings = 1000;
    [Tooltip("Сумма, получаемая за нанесение урона игроку")]
    [SerializeField] private int _playerDamageReward = 100;

    private EnemiseSpawnerControlledRandom _controlledRandom;
    private float _timeElapsed = 0.0f;
    private int _nextStack;
    private int _playerSpend = 0;

    private void Awake()
    {
        _controlledRandom = GetComponent<EnemiseSpawnerControlledRandom>();
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= 1.0f)
        {
            _money += _moneyPerSecond;
            _timeElapsed = 0.0f;
        }

        if (_money > _enemies[_nextStack].Cost)
        {
            SpawnNextStack();
        }
    }

    private void SelectNextStack()
    {
        if (_playerSpend > _levelSpendings)
        {
            //turn this off and launch exit sequence
            Destroy(this);
            return;
        }
        int nextStack = Mathf.RoundToInt(_controlledRandom.CalculateRandomValue() * _enemies.Length);
        _nextStack = nextStack;
    }

    private void SpawnNextStack()
    {
        StackOfEnemies enemy = _enemies[_nextStack];
        _money -= enemy.Cost;
        _playerSpend += enemy.Cost;
        GameObject stack = Instantiate(enemy.StackPrefab);
        float screenWidth = Camera.main.orthographicSize / Camera.main.aspect;
        stack.transform.position = transform.position.With(x: Random.Range(-screenWidth + enemy.Width, screenWidth - enemy.Width));
        SelectNextStack();
    }

    //Как-то перевесить на событие?
    public void PlayerTookDamage()
    {
        _money += _playerDamageReward;
    }
}
