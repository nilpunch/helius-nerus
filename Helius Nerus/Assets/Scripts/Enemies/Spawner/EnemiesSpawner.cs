using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	public static event System.Action MoneyRunOut = delegate { };

	[Tooltip("Параметры контроллируемого рандома")]
	[SerializeField] private EnemiseSpawnerControlledRandom _controlledRandom = null;

	[Tooltip("СО с new пачками врагов")]
	[SerializeField] private PackScriptableObject[] _enemiesPacksNew = null;
	[Tooltip("Начальное количество денег")]
	[SerializeField] private float _money = 10;
	[Tooltip("Доход в секунду")]
	[SerializeField] private float _moneyPerSecond = 10;
	[Tooltip("Сумма трат за уровнень")]
	[SerializeField] private float _moneySpendingLimit = 1000;
	[Tooltip("Сумма, получаемая за нанесение урона игроку")]
	[SerializeField] private float _playerDamageReward = 100;

	private float _screenWidth;

	private int _nextPackIndex;
	private float _currentMoneySpent = 0;
	private Transform _transform;
	private float _moneyPerSecondMultyplier = 1f;

	public static EnemiesSpawner InstanceOnScene { get; private set; }

	private void Awake()
	{
		// На каждой новой сцене (при гарантии что спаунер 1) глобальная ссылка будет перезаписываться на необходимую
		InstanceOnScene = this;
		_transform = transform;
		_transform.position = new Vector3(0.0f, ParallaxCamera.ParallaxSize.y / 2, 0.0f);
		_screenWidth = ParallaxCamera.ParallaxSize.x / 2;

		Player.PlayerTookDamage += Player_PlayerTookDamage;

		System.Array.Sort(_enemiesPacksNew, (pack1, pack2) => pack1.Cost - pack2.Cost);

		SelectNextPack();
		_money = _enemiesPacksNew[_nextPackIndex].Cost;

        Player.PlayerDie += Player_PlayerDie;

		EnemiesInSceneCounter.EnemyCountChanged += EnemiesInSceneCounter_EnemyCountChanged;
	}

	private void EnemiesInSceneCounter_EnemyCountChanged()
	{
		if (Level.EnemiesCounter.AmountOfEnemies == 0 && MoneyLimitNotReached())
		{
			_moneyPerSecondMultyplier = 3f;
		}
		else
		{
			_moneyPerSecondMultyplier = 1f;
		}
	}

	private void Player_PlayerDie()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
	{
		Player.PlayerTookDamage -= Player_PlayerTookDamage;
        Player.PlayerDie -= Player_PlayerDie;
    }

	private void Update()
	{
		if (MoneyLimitNotReached())
		{
			_money += _moneyPerSecond * TimeManager.EnemyDeltaTime * _moneyPerSecondMultyplier;
		}

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

	private void Player_PlayerTookDamage()
	{
		_money += _playerDamageReward;
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
			newEnemy.transform.localEulerAngles = new Vector3(0f, 0f, nextNewPack.Enemies[i].Rotation);
		}
	}

	private void SelectNextPack()
	{
		int nextPackIndex = Mathf.RoundToInt(_controlledRandom.CalculateRandomValue(Level.EnemiesCounter.AmountOfEnemies) * (_enemiesPacksNew.Length - 1));
		_nextPackIndex = nextPackIndex;
	}
}