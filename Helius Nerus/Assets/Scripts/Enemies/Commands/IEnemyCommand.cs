using UnityEngine;

public interface IEnemyCommand
{
	bool WorkOnce { get; }

	void Tick(Transform transform);
	bool IsEnded();
	void Reset();
}