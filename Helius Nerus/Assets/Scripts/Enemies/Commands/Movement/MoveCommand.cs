using UnityEngine;

public abstract class MoveCommand : IEnemyCommand
{
	protected MoveCommandData CommandData;

	public bool WorkOnce => CommandData.WorkOnce;

    public void SetParametrs(MoveCommandData commandData)
    {
		CommandData = commandData.Clone();
	}

	public abstract void Tick(Transform ship);
    public abstract bool IsEnded();
    public abstract void Reset();

    public MoveCommand Clone()
    {
        return (MoveCommand)this.MemberwiseClone();
    }
}