using System.Collections.Generic;
using UnityEngine;

public class BulletPoolsContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] _bulletsPrefabs = null;
    private List<BulletPool> _pools = new List<BulletPool>();
    private Transform _transform;

    public static BulletPoolsContainer Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _transform = transform;

        for (int i = 0; i < _bulletsPrefabs.Length; ++i)
        {
            _pools.Add(new BulletPool(_bulletsPrefabs[i], _transform));
        }

        TransitionScene.NewSceneWasLoaded += TransitionScene_NewSceneWasLoaded;
    }

    private void OnEnable()
    {
        Player.PlayerDie += Player_PlayerDie;
        LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
    }

    private void LevelBoss_FinalBossDied(int obj)
    {
        ClearAllBullets();
    }

    private void OnDisable()
    {
        Player.PlayerDie -= Player_PlayerDie;
    }

    private void Player_PlayerDie()
    {
        ClearAllBullets();
    }

    private void TransitionScene_NewSceneWasLoaded(Scenes obj)
    {
        ClearAllBullets();
    }

    public GameObject GetObjectFromPool(BulletTypes type)
    {
        return _pools[(int)type].GetObjectFromPool();
    }

    public void ReturnObjectToPool(BulletTypes type, GameObject go)
    {
        _pools[(int)type].ReturnObjectToPool(go);
    }

    public void ClearAllBullets()
    {
        foreach (BulletPool bulletPool in _pools)
        {
            bulletPool.ClearScreenBullets();
        }
    }

    private void OnDestroy()
    {
        TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
    }
}

public class BulletPool
{
    private GameObject _prefab = null;
    private Transform _transform = null;
    private Queue<GameObject> _pool = new Queue<GameObject>();

    // Use another collection?
    private List<GameObject> _registeredBullets = new List<GameObject>();

    public BulletPool(GameObject prefab, Transform transform)
    {
        _prefab = prefab;
        _transform = transform;
    }

    private void AddObjectToPool(int amount)
    {
        GameObject go;
        for (int i = 0; i < amount; ++i)
        {
            go = GameObject.Instantiate(_prefab);
            go.transform.parent = _transform;
            go.SetActive(false);
            _pool.Enqueue(go);
            _registeredBullets.Add(go);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (_pool.Count > 0)
        {
            GameObject go;
            go = _pool.Dequeue();
            go.SetActive(true);
            go.transform.parent = null;

            //_registeredBullets.Add(go);

            return go;
        }
        else
        {
            AddObjectToPool(10);
            return GetObjectFromPool();
        }
    }

    public void ReturnObjectToPool(GameObject go)
    {
        go.transform.parent = _transform;
        go.SetActive(false);
        //_registeredBullets.Remove(go);
        _pool.Enqueue(go);
    }

    public void ClearScreenBullets()
    {
        foreach(GameObject go in _registeredBullets)
        {
            if (go.activeSelf == true)
                ReturnObjectToPool(go);
        }
        //_registeredBullets.Clear();
    }
}

public enum BulletTypes
{
    StraightMove,
    PlayerBullet,
}