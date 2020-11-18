using UnityEngine;

public enum InputType
{
    Keyboard,
    Mouse,
    DragMovement,
    FloatingJoystick,
    FixedJoystick,
};

[System.Serializable]
public class PlayerMovement
{
    [SerializeField] private Transform _transform = null;
    [SerializeField] private float _playerBoundaryOffset = 0.0f;
    [SerializeField] private float _sensivity = 1.0f;
    [SerializeField] private InputType _moveInputType = InputType.Mouse;

    private IMoveInput _moveInput = null;
    private TransformMover _transformMover = null;

    public void Init()
    {
        switch (_moveInputType)
        {
            case InputType.Keyboard:
                _moveInput = new KeyboradMoveInput();
                break;

            case InputType.Mouse:
                _moveInput = new MouseMoveInput(_transform);
                break;

            case InputType.DragMovement:
                DragMovement.Activate();
                _moveInput = (IMoveInput)DragMovement.Instance;
                break;

            case InputType.FloatingJoystick:
                FloatingJoystick.Activate();
                _moveInput = (IMoveInput)FloatingJoystick.Instance;
                break;

            case InputType.FixedJoystick:
                FixedJoystick.Activate();
                _moveInput = (IMoveInput)FixedJoystick.Instance;
                break;
        }

        PlayerPrefs.SetString("InputType", _moveInputType.ToString());

        _transformMover = new TransformMover(_moveInput, _transform, _sensivity);
    }

    public void Update()
    {
        _moveInput.ReadInput();
        _transformMover.Tick();

        _transform.position = new Vector3(
            Mathf.Clamp(_transform.position.x, ParallaxCamera.ParallaxBoundary.xMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.xMax - _playerBoundaryOffset),
            Mathf.Clamp(_transform.position.y, ParallaxCamera.ParallaxBoundary.yMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.yMax - _playerBoundaryOffset),
            _transform.position.z);

        ParallaxCamera.CameraTransform.position = ParallaxCamera.CameraTransform.position.With(x: _transform.position.x * ParallaxCamera.TranslationCoefficient);
    }
}