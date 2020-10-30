public abstract class PlayerArtifact
{
	public virtual void OnPick() { }
	public virtual void OnDrop() { }
	public abstract PlayerArtifact Clone();
}