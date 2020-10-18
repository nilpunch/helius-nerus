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
		_moveInput.ReadInput();
		_transformMover.Tick();

		float unclampedPositionX = _transform.position.x;
		_transform.position = new Vector3(
			Mathf.Clamp(_transform.position.x, CameraParallaxFollow.GameBoundaryRect.xMin + _playerBoundaryOffset, CameraParallaxFollow.GameBoundaryRect.xMax - _playerBoundaryOffset),
			Mathf.Clamp(_transform.position.y, CameraParallaxFollow.GameBoundaryRect.yMin + _playerBoundaryOffset, CameraParallaxFollow.GameBoundaryRect.yMax - _playerBoundaryOffset),
			_transform.position.z);

		if (unclampedPositionX == _transform.position.x)
		{
			CameraParallaxFollow.CameraTransform.Translate(_transformMover.LastDeltaMovement.x * CameraParallaxFollow.TranslationCoefficient, 0.0f, 0.0f, Space.World);
		}
	}
}
