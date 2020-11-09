public abstract class PlayerArtifact
{
    public abstract string MyEnumName
    {
        get;
    }

	public virtual void OnPick() { }
	public virtual void OnDrop() { }
	public abstract PlayerArtifact Clone();
}