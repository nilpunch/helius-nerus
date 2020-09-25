interface IEnemyCommand
{
	bool WorkOnce { get; }
	void Tick();
	bool IsEnded();
}