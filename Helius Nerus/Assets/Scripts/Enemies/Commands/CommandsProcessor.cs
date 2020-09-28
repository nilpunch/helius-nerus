using UnityEngine;

[System.Serializable]
public class CommandsProcessor<CommandParameters> where CommandParameters : ICommandParameters
{
    [SerializeField] private ProcessorThread<CommandParameters>[] _threads = null;

    private Transform _enemyShip = null;

    public void Initialize(Transform enemyShip)
    {
        _enemyShip = enemyShip;

        for (int i = 0; i < _threads.Length; ++i)
            _threads[i].Initialize(_enemyShip);
    }

    public void TickCommandThreads()
    {
        for (int i = 0; i < _threads.Length; ++i)
            _threads[i].TickCommand();
    }
}