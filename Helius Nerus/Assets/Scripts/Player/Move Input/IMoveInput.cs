using UnityEngine;

public interface IMoveInput
{
	Vector2 Direction { get; }
	float Thrust { get; }
	void ReadInput();
}
