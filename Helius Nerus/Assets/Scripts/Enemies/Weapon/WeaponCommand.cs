using UnityEngine;

public abstract class WeaponCommand : IEnemyCommand
{
	protected WeaponCommandData _commandData;
	protected float _timeDelayBefore = 0.0f;
	protected float _timeDelayAfter = 0.0f;

	public bool WorkOnce { get; private set; }

	public abstract void Tick();
	public abstract bool IsEnded();
	public abstract void Reset();
}