using System;
using UnityEngine;

// Do Nothing Command
class EmptyEnemyCommand : IEnemyCommand
{
	public bool WorkOnce => true;

	public bool IsEnded()
	{
		return true;
	}

	public void Reset()
	{
	}

	public void Tick(Transform transform)
	{
	}
}