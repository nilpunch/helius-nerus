using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IMoveInput
{
    [SerializeField] private Canvas _joystickCanvas = null;
    [SerializeField] private RectTransform _buttonTransform = null;
    [SerializeField] private RectTransform _baseTransform = null;
    [SerializeField] private float _maxDistance = 5.0f;

    private Vector2 _direction;

    public Vector2 Direction
    {
        get;
        private set;
    }

    public float Thrust
    {
        get;
        private set;
    }

    public static FloatingJoystick Instance
    {
        get;
        private set;
    } = null;

    private void Awake()
    {
        Instance = this;
        Deactivate();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetString("InputType") != "FloatingJoystick")
            Deactivate();
    }

    public static void Activate()
    {
        Instance._joystickCanvas.enabled = true;
    }

    public static void Deactivate()
    {
        Instance._joystickCanvas.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetButtonPosition(eventData.position);
    }

    public void ReadInput()
    {
        Direction = _direction.normalized;
        Thrust = _direction.magnitude / _maxDistance;
    }

    private void SetButtonPosition(Vector2 position)
    {
        _direction = position - _baseTransform.anchoredPosition;
        _direction = _direction.ClampInBorders(_maxDistance);
        _buttonTransform.anchoredPosition = _direction;
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		_baseTransform.anchoredPosition = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_buttonTransform.anchoredPosition = Vector2.zero;
		_direction = Vector2.zero;
	}
}
