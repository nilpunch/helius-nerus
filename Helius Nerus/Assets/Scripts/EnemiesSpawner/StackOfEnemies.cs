using UnityEngine;

[CreateAssetMenu(fileName = "Stack_1", menuName = "ScriptableObjects/StackOfEnemiesScriptableObject", order = 1)]
public class StackOfEnemies : ScriptableObject
{
    public GameObject StackPrefab => _stackPrefab;
    public float Width => _width;
    public int Cost => _cost;
    [SerializeField] private GameObject _stackPrefab = null;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private int _cost = 10;
}
