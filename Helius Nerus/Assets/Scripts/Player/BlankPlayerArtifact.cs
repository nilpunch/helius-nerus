class BlankPlayerArtifact : IPlayerArtifact
{
	public void OnPick(Player player)
	{
	}
	public void OnTick(Player player)
	{
	}
	public void OnDrop(Player player)
	{
	}

	public IPlayerArtifact Clone()
	{
		return (InvincibilityArtifact)this.MemberwiseClone();
	}
}