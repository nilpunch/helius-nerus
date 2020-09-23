using UnityEngine;

[CreateAssetMenu(fileName = "Stack_1", menuName = "ScriptableObjects/StackOfEnemiesScriptableObject", order = 1)]
public class StackOfEnemies : ScriptableObject
{
    [SerializeField] private int _cost = 10;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private GameObject _stackPrefab = null;
    public int Cost => _cost;
    public float Width => _width;
    public GameObject StackPrefab => _stackPrefab;
}
