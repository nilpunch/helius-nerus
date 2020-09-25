using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementCommandsProcessor
{
    //[System.Serializable]
    //private struct CommandParametrs
    //{
    //    [SerializeField] private string _commandType;
    //    [SerializeField] private float _movementMultiplier;
    //    [SerializeField] private float _timeScale;
    //    [SerializeField] private float _endParameter;
    //    [SerializeField] private bool _workOnce;

    //    public MovementCommand CommandParamsToCommand()
    //    {
    //        Type comType = Type.GetType(_commandType + "MovementCommand");
    //        MovementCommand instance = (MovementCommand)Activator.CreateInstance(comType);
    //        instance.SetParametrs(_movementMultiplier, _timeScale, _endParameter, _workOnce);

    //        return instance;
    //    }
    //}

    //[System.Serializable]
    //private class ListWrapper
    //{
    //    public List<CommandParametrs> myList;
    //}

    //[Tooltip("Потоки процессора команд")]
    //[SerializeField] private List<ListWrapper> _threads = new List<ListWrapper>();

    //private List<MovementCommand>[] _movementCommandsThreads = null;
    //private int[] _iterators = null;
    [SerializeField] private ProcessorThread[] _threads = null;


    private Transform _enemyShip = null;

    public void Initialize(Transform enemyShip)
    {
        _enemyShip = enemyShip;

        //_movementCommandsThreads = new List<MovementCommand>[_threads.Count];
        //for (int i = 0; i < _movementCommandsThreads.Length; ++i)
        //{
        //    for (int j = 0; j < _threads[i].myList.Count; ++j)
        //    {
        //        _movementCommandsThreads[i].Add(_threads[i].myList[j].CommandParamsToCommand());
        //    }
        //    _iterators[i] = 0;
        //}
        for (int i = 0; i < _threads.Length; ++i)
            _threads[i].Initialize(_enemyShip);
    }

    // Update is called once per frame
    public void Tick()
    {
        //for (int i = 0; i < _threads.Count; ++i)
        //{
        //    _movementCommandsThreads[i][_iterators[i]].Tick(_enemyShip);
        //    if (_movementCommandsThreads[i][_iterators[i]].IsEnded())
        //    {
        //        if (_movementCommandsThreads[i][_iterators[i]].WorkOnce)
        //        {
        //            _movementCommandsThreads[i].RemoveAt(_iterators[i]);
        //        }
        //        else
        //        {
        //            _iterators[i]++;
        //            if (_iterators[i] == _movementCommandsThreads[i].Count)
        //                _iterators[i] = 0;
        //        }
        //    }
        //}
        for (int i = 0; i < _threads.Length; ++i)
            _threads[i].Tick();
    }
}