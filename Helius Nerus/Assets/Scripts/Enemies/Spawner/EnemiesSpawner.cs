using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	public static EnemiesSpawner InstanceOnScene { get; private set; }

	[Tooltip("Параметры контроллируемого рандома")]
	[SerializeField] private EnemiseSpawnerControlledRandom _controlledRandom = null;

	[Tooltip("СО с пачками врагов")]
	[SerializeField] private PackOfEnemies[] _enemiesPacks = null;
	[Tooltip("Начальное количество денег")]
	[SerializeField] private int _money = 10;
	[Tooltip("Доход в секунду")]
	[SerializeField] private int _moneyPerSecond = 10;
	[Tooltip("Сумма трат за уровнень")]
	[SerializeField] private int _moneySpendingLimit = 1000;
	[Tooltip("Сумма, получаемая за нанесение урона игроку")]
	[SerializeField] private int _playerDamageReward = 100;

	public event System.Action MoneyRunOut = delegate { };

	private float _timeBeforeReceivingMoney = 0.0f;

	private int _nextPackIndex;
	private int _currentMoneySpent = 0;


	private void Awake()
	{
		// На каждой новой сцене (при гарантии что спаунер 1) глобальная ссылка будет перезаписываться на необходимую
		InstanceOnScene = this;
	}

	private void Start()
	{
		// Сортировка пачек врагов по возрастанию
		System.Array.Sort(_enemiesPacks, (pack1, pack2) => pack1.Cost - pack2.Cost);

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

			int nextPackCost = _enemiesPacks[_nextPackIndex].Cost;
			if (_money > nextPackCost)
			{
				// Пересчитываем деньги
				_money -= nextPackCost;
				_currentMoneySpent += nextPackCost;
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
		PackOfEnemies nextPack = _enemiesPacks[_nextPackIndex];
		GameObject packGameObject = Instantiate(nextPack.PackPrefab);
		float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
		packGameObject.transform.position = transform.position.With(x: Random.Range(-screenWidth + nextPack.Width, screenWidth - nextPack.Width));
	}

	private void SelectNextPack()
	{
		int nextPackIndex = Mathf.RoundToInt(_controlledRandom.CalculateRandomValue(Game_Temp.Instance.EnemiesCounter.AmountOfEnemies) * (_enemiesPacks.Length - 1));
        _nextPackIndex = nextPackIndex;
	}

	//Как-то перевесить на событие?
	public void PlayerTookDamage()
	{
		_money += _playerDamageReward;
	}
}
