public interface IPlayerArtifact
{
	void OnPick(Player player);
	void OnTick(Player player);
	void OnDrop(Player player);
	IPlayerArtifact Clone();
}