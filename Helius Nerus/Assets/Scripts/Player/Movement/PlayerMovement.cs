using UnityEngine;

public enum InputType
{
    DragMovement = 0,
    FixedJoystick = 1,
    FloatingJoystick = 2,
    Keyboard,
    Mouse,
};

[System.Serializable]
public class PlayerMovement
{
    [SerializeField] private Transform _transform = null;
    [SerializeField] private float _playerBoundaryOffset = 0.0f;
    [SerializeField] private float _sensivity = 1.0f;
    [SerializeField] private InputType _moveInputType = InputType.Mouse;

    private IMoveInput _moveInput = null;

    public void Init()
    {
        if (PlayerPrefs.HasKey("ControlType"))
        {
            _moveInputType = (InputType)System.Enum.Parse(typeof(InputType), PlayerPrefs.GetString("ControlType"));
        }
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            _sensivity = PlayerPrefs.GetFloat("Sensitivity");
        }

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
    }

    public void Update()
    {
        _moveInput.ReadInput();
		_moveInput.Tick(_transform, _sensivity);

        _transform.position = new Vector3(
            Mathf.Clamp(_transform.position.x, ParallaxCamera.ParallaxBoundary.xMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.xMax - _playerBoundaryOffset),
            Mathf.Clamp(_transform.position.y, ParallaxCamera.ParallaxBoundary.yMin + _playerBoundaryOffset, ParallaxCamera.ParallaxBoundary.yMax - _playerBoundaryOffset),
            _transform.position.z);

        ParallaxCamera.CameraTransform.position = ParallaxCamera.CameraTransform.position.With(x: _transform.position.x * ParallaxCamera.TranslationCoefficient);
    }
}