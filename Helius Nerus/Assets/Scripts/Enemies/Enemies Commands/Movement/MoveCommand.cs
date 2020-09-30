using UnityEngine;

[System.Serializable]
public abstract class MoveCommand : IEnemyCommand
{
	[SerializeField] protected MoveCommandData CommandData;

	public bool WorkOnce => CommandData.WorkOnce;

    public void SetParametrs(MoveCommandData commandData)
    {
		CommandData = commandData;
	}

	public abstract void Tick(Transform ship);
    public abstract bool IsEnded();
    public abstract void Reset();

    // Недостающий метод
    public MoveCommand Clone()
    {
        return (MoveCommand)this.MemberwiseClone();
    }
}