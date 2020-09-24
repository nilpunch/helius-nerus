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

	private float _timeBforeReceivingMoney = 0.0f;

	private int _nextPackIndex;
	private int _currentMoneySpent = 0;

	// На каждой новой сцене глобальная ссылка будет перезаписываться
	private void Awake()
	{
		InstanceOnScene = this;
	}

	private void Start()
	{

	}

	private void Update()
	{
		_timeBforeReceivingMoney += Time.deltaTime;
		if (_timeBforeReceivingMoney >= 1.0f)
		{
			_money += _moneyPerSecond;
			_timeBforeReceivingMoney = 0.0f;
		}

		int spendings = _enemiesPacks[_nextPackIndex].Cost;
		if (_money > spendings)
		{
			if (TrySpendMoney(spendings))
			{
				SpawnNextPack();
				SelectNextPack();
			}
		}
	}

	private bool TrySpendMoney(int amount)
	{
		if (_currentMoneySpent > _moneySpendingLimit)
		{
			MoneyRunOut.Invoke();
			return false;
		}
		else
		{
			_money -= amount;
			_currentMoneySpent += amount;
			return true;
		}
	}

	private void SpawnNextPack()
	{
		PackOfEnemies nextPack = _enemiesPacks[_nextPackIndex];

		GameObject packGameObject = Instantiate(nextPack.PackPrefab);
		float screenWidth = Camera.main.orthographicSize / Camera.main.aspect;
		packGameObject.transform.position = transform.position.With(x: Random.Range(-screenWidth + nextPack.Width, screenWidth - nextPack.Width));
	}

	private void SelectNextPack()
	{
		int nextPackIndex = Mathf.RoundToInt(_controlledRandom.CalculateRandomValue() * _enemiesPacks.Length);
		_nextPackIndex = nextPackIndex;
	}

	//Как-то перевесить на событие?
	public void PlayerTookDamage()
	{
		_money += _playerDamageReward;
	}
}
