using UnityEngine;

[CreateAssetMenu(fileName = "Pack", menuName = "ScriptableObjects/New enemies pack", order = 1)]
public class PackScriptableObject : ScriptableObject
{
    public EnemyInPack[] Enemies
    {
        get => _enemies;
    }
    public float Width => _width;
    public int Cost => _cost;
    [SerializeField] private EnemyInPack[] _enemies = null;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private int _cost = 10;
}

[System.Serializable]
public class EnemyInPack
{
    public Vector2 Position
    {
        get => _position;
    }
    public EnemyTypes Type
    {
        get => _type;
    }
    [SerializeField] private Vector2 _position = Vector2.zero;
    [SerializeField] private EnemyTypes _type = EnemyTypes.SquareEnemy;
}
