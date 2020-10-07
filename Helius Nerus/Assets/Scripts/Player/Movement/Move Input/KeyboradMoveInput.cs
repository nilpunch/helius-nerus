using UnityEngine;

class KeyboradMoveInput : IMoveInput
{
	public Vector2 Direction { get; private set; }
	public float Thrust { get; private set; }

	public KeyboradMoveInput(KeyboardMoveSettings keyboardMoveSettings)
	{
		Thrust = Mathf.Clamp01(keyboardMoveSettings.Thrust);
	}

	public void ReadInput()
	{
		Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Direction = axis.normalized;
		Thrust = Mathf.Clamp01(axis.magnitude);
	}
}

