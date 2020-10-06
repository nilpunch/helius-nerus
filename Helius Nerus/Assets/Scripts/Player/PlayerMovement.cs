using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private MoveParameters MoveParameters = null;
	[System.Serializable] public enum InputType { Keyboard, Mouse };
	[SerializeField] private InputType MoveInputType = InputType.Mouse;
	[SerializeField] private MouseMoveSettings MouseMoveSettings = null;
	[SerializeField] private KeyboardMoveSettings KeyboradMoveSettings = null;

	private IMoveInput _moveInput = null;
	private TransformMover _transformMover = null;

	void Start()
    {
		switch (MoveInputType)
		{
		case InputType.Keyboard:
			_moveInput = new KeyboradMoveInput(KeyboradMoveSettings);
			break;

		case InputType.Mouse:
			_moveInput = new MouseMoveInput(transform, MouseMoveSettings);
			break;
		}

		_transformMover = new TransformMover(_moveInput, transform, MoveParameters);
	}

    void Update()
    {
		_moveInput.ReadInput();
		_transformMover.Tick();
	}

	public void ChangeMoveInput(IMoveInput newMoveInput)
	{
		_moveInput = newMoveInput;
		_transformMover = new TransformMover(_moveInput, transform, MoveParameters);
	}
}
