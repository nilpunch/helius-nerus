using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
	[SerializeField] private Transform Transform = null;
	[SerializeField] private MoveParameters MoveParameters = null;
	[System.Serializable] public enum InputType { Keyboard, Mouse };
	[SerializeField] private InputType MoveInputType = InputType.Mouse;
	[SerializeField] private MouseMoveSettings MouseMoveSettings = null;
	[SerializeField] private KeyboardMoveSettings KeyboradMoveSettings = null;

	private IMoveInput _moveInput = null;
	private TransformMover _transformMover = null;

	public void Init()
    {
		switch (MoveInputType)
		{
		case InputType.Keyboard:
			_moveInput = new KeyboradMoveInput(KeyboradMoveSettings);
			break;

		case InputType.Mouse:
			_moveInput = new MouseMoveInput(Transform, MouseMoveSettings);
			break;
		}

		_transformMover = new TransformMover(_moveInput, Transform, MoveParameters);
	}

    public void Update()
    {
		_moveInput.ReadInput();
		_transformMover.Tick();
	}
}
