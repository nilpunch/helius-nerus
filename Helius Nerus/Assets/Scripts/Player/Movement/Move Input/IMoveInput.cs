using UnityEngine;

public interface IMoveInput
{
	void ReadInput();
	void Tick(Transform transform, float sensitivity);
}
