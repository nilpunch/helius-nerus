using System.Collections;

public interface IPlayerArtifact
{
	void OnPick();
	IEnumerator OnProc();
	void OnDrop();
	IPlayerArtifact Clone();
}