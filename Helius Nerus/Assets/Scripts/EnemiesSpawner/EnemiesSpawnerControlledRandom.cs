using UnityEngine;

[System.Serializable]
public class EnemiseSpawnerControlledRandom
{
    [Tooltip("Высота основной полосы спауна")]
    [SerializeField] private float _mainStripHeight = 1.0f; // h2
    [Tooltip("Высота дополнительной полосы спауна")]
    [SerializeField] private float _secondStripHeight = 0.2f; // h1
    [Tooltip("Ширина основной полосы спауна")]
    [Range(0.0f, 0.5f)]
    [SerializeField] private float _mainStripLength = 0.2f; // l
    [Tooltip("Количество врагов, при котором x принимает максимальное значение")]
    [SerializeField] private float _maxEnemiesAmount = 100.0f;

    public float CalculateRandomValue()
    {
        float x = (1 - 2 * _mainStripLength) / _maxEnemiesAmount * Mathf.Clamp(EnemiesInSceneCounter.AmountOfEnemies, 0.0f, _maxEnemiesAmount) + _mainStripLength;
        float areaOfFigure = 2 * _mainStripLength * (_mainStripHeight - _secondStripHeight) + _secondStripHeight;
        float rand = Random.Range(0, areaOfFigure);

        if (rand < (x - _mainStripLength) * _secondStripHeight)
        {
            return rand / _secondStripHeight;
        }
        else if (rand > (x - _mainStripLength) * _secondStripHeight + 2 * _mainStripLength * _mainStripHeight)
        {
            return x + _mainStripLength + (rand - ((x - _mainStripLength) * _secondStripHeight + 2 * _mainStripLength * _mainStripHeight)) / _secondStripHeight;
        }
        else
        {
            return x - _mainStripLength + (rand - (x - _mainStripLength) * _secondStripHeight) / _mainStripHeight;
        }
    }
}
