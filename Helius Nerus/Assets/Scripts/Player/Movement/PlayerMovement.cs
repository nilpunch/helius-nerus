using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
	[SerializeField] private Transform _transform = null;
	[SerializeField] private float _playerBoundaryOffset = 0.0f;
	[SerializeField] private MoveParameters _moveParameters = null;
	[System.Serializable] public enum InputType { Keyboard, Mouse };
	[SerializeField] private InputType _moveInputType = InputType.Mouse;
	[SerializeField] private MouseMoveSettings _mouseMoveSettings = null;
	[SerializeField] private KeyboardMoveSettings _keyboradMoveSettings = null;

	private IMoveInput _moveInput = null;
	private TransformMover _transformMover = null;

	public void Init()
	{
		switch (_moveInputType)
		{
		case InputType.Keyboard:
			_moveInput = new KeyboradMoveInput(_keyboradMoveSettings);
			break;

		case InputType.Mouse:
			_moveInput = new MouseMoveInput(_transform, _mouseMoveSettings);
			break;
		}

		_transformMover = new TransformMover(_moveInput, _transform, _moveParameters);
	}

	public void Update()
	{
        if (Pause.Paused)
            return;

		_moveInput.ReadInput();

		float oldPosition = _transform.position.x;
		_transformMover.Tick();

		_transform.position = new Vector3(
			Mathf.Clamp(_transform.position.x, ParallaxCamera.ParallaxBoundary.xMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.xMax - _playerBoundaryOffset),
			Mathf.Clamp(_transform.position.y, ParallaxCamera.ParallaxBoundary.yMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.yMax - _playerBoundaryOffset),
			_transform.position.z);

		ParallaxCamera.CameraTransform.position = ParallaxCamera.CameraTransform.position.With(x: _transform.position.x * ParallaxCamera.TranslationCoefficient);
	}
}
