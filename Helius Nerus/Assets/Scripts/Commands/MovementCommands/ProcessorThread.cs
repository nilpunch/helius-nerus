using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProcessorThread
{
    [System.Serializable]
    private struct CommandParametrs
    {
        [SerializeField] private string _commandType;
        [SerializeField] private float _movementMultiplier;
        [SerializeField] private float _timeScale;
        [SerializeField] private float _endParameter;
        [SerializeField] private bool _workOnce;

        public MovementCommand CommandParamsToCommand()
        {
            Type comType = Type.GetType(_commandType + "MovementCommand");
            MovementCommand instance = (MovementCommand)Activator.CreateInstance(comType);
            instance.SetParametrs(_movementMultiplier, _timeScale, _endParameter, _workOnce);

            return instance;
        }
    }

    [SerializeField] private List<CommandParametrs> _commandParametrs = new List<CommandParametrs>();

    private List<MovementCommand> _commands = new List<MovementCommand>();

    private int _iterator = 0;
    private Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;

        for (int i = 0; i < _commandParametrs.Count; ++i)
        {
            _commands.Add(_commandParametrs[i].CommandParamsToCommand());
        }

        //_commandParametrs.Clear();
    }


    public void Tick()
    {
        if (_commands.Count == 0)
            return;
        _commands[_iterator].Tick(_transform);
        if (_commands[_iterator].IsEnded())
        {
            if (_commands[_iterator].WorkOnce)
            {
                _commands.RemoveAt(_iterator);
            }
            else
            {
                _commands[_iterator].Reset();
                ++_iterator;
                if (_iterator >= _commands.Count)
                    _iterator = 0;
            }
        }
    }

}