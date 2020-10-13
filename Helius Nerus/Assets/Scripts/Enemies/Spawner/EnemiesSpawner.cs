using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	public static EnemiesSpawner InstanceOnScene { get; private set; }

	[Tooltip("Параметры контроллируемого рандома")]
	[SerializeField] private EnemiseSpawnerControlledRandom _controlledRandom = null;

    [Tooltip("СО с new пачками врагов")]
    [SerializeField] private PackScriptableObject[] _enemiesPacksNew = null;
    [Tooltip("Начальное количество денег")]
	[SerializeField] private int _money = 10;
	[Tooltip("Доход в секунду")]
	[SerializeField] private int _moneyPerSecond = 10;
	[Tooltip("Сумма трат за уровнень")]
	[SerializeField] private int _moneySpendingLimit = 1000;
	[Tooltip("Сумма, получаемая за нанесение урона игроку")]
	[SerializeField] private int _playerDamageReward = 100;

	public static event System.Action MoneyRunOut = delegate { };

	private float _timeBeforeReceivingMoney = 0.0f;
    private float _screenWidth;

	private int _nextPackIndex;
	private int _currentMoneySpent = 0;
    private Transform _transform;


	private void Awake()
	{
		// На каждой новой сцене (при гарантии что спаунер 1) глобальная ссылка будет перезаписываться на необходимую
		InstanceOnScene = this;
        _transform = transform;
        _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        Player.PlayerTookDamage += PlayerTookDamage;
    }

    private void OnDestroy()
    {
        Player.PlayerTookDamage -= PlayerTookDamage;
    }

    private void Start()
	{
        System.Array.Sort(_enemiesPacksNew, (pack1, pack2) => pack1.Cost - pack2.Cost);

        SelectNextPack();
	}

	private void Update()
	{
		_timeBeforeReceivingMoney += Time.deltaTime;
		if (_timeBeforeReceivingMoney >= 1.0f)
		{
			if (MoneyLimitNotReached())
			{
				_money += _moneyPerSecond;
			}
			_timeBeforeReceivingMoney = 0.0f;

			while (_money > _enemiesPacksNew[_nextPackIndex].Cost)
			{
				// Пересчитываем деньги
				_money -= _enemiesPacksNew[_nextPackIndex].Cost;
				_currentMoneySpent += _enemiesPacksNew[_nextPackIndex].Cost;
                SpawnNextPack();
                SelectNextPack();
                if (MoneyLimitNotReached() == false)
				{
					// Обнулить деньги чтобы гарантировать единоразовый вызов события MoneyRunOut()
					_money = 0;
					MoneyRunOut.Invoke();
				}
			}
		}
    }

	private bool MoneyLimitNotReached()
	{
		return _currentMoneySpent < _moneySpendingLimit;
	}

	private void SpawnNextPack()
	{
        PackScriptableObject nextNewPack = _enemiesPacksNew[_nextPackIndex];
        GameObject newEnemy;
        float randomOffset = Random.Range(-_screenWidth + nextNewPack.Width, _screenWidth - nextNewPack.Width);
        for (int i = 0; i < nextNewPack.Enemies.Length; ++i)
        {
            newEnemy = EnemyPoolContainer.Instance.GetObjectFromPool(nextNewPack.Enemies[i].Type);
            newEnemy.transform.parent = _transform;
            newEnemy.transform.localPosition = nextNewPack.Enemies[i].Position + new Vector2(randomOffset, 0);
        }
	}

	private void SelectNextPack()
	{
		int nextPackIndex = Mathf.RoundToInt(_controlledRandom.CalculateRandomValue(Level.Instance.EnemiesCounter.AmountOfEnemies) * (_enemiesPacksNew.Length - 1));
        _nextPackIndex = nextPackIndex;
	}

	//Как-то перевесить на событие?
	public void PlayerTookDamage()
	{
		_money += _playerDamageReward;
	}
}