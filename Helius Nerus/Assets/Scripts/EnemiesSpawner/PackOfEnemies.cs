using UnityEngine;

[CreateAssetMenu(fileName = "Pack_1", menuName = "ScriptableObjects/PackOfEnemiesScriptableObject", order = 1)]
public class PackOfEnemies : ScriptableObject
{
    public GameObject PackPrefab => _packPrefab;
    public float Width => _width;
    public int Cost => _cost;
    [SerializeField] private GameObject _packPrefab = null;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private int _cost = 10;
}
