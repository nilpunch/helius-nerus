using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProcessorThread<CommandParameters> where CommandParameters : ICommandParameters
{
	[SerializeField] private List<CommandParameters> _commandParametrs = new List<CommandParameters>();

    // тута валялись новые команды
    [SerializeField] private List<MoveCommandType> _commands2 = new List<MoveCommandType>();

	private List<IEnemyCommand> _commands = new List<IEnemyCommand>();

	private int _iterator = 0;
	private Transform _transform;

	public void Initialize(Transform transform)
	{
		_transform = transform;

		for (int i = 0; i < _commandParametrs.Count; ++i)
		{
			_commands.Add(_commandParametrs[i].CreateCommand());
		}

		//_commandParametrs.Clear();
	}

    public void InitializeNewWay(Transform transform)
    {
        //_transform = transform;

        //for (int i = 0; i < _commands2.Count; ++i)
        //{
        //    MoveCommand com = CreateMovementCommand.GetCommandByEnum(_commands2[i]);
        //    com.SetParametrs(new MoveCommandData(1, 1, 1, false));
        //    _commands.Add(com);
        //}
    }

    public void TickCommand()
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