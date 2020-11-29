using UnityEngine;
using UnityEngine.EventSystems;

public class DragMovement : InputCanvas<DragMovement>, IPointerUpHandler, IPointerDownHandler, IDragHandler, IMoveInput
{
	private Vector2 _startPosition = Vector2.zero;
	private Vector2 _delta = Vector2.zero;

	protected override string InputType
    {
        get => "DragMovement";
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		_startPosition = eventData.position;
		_delta = Vector2.zero;
	}

	public void OnPointerUp(PointerEventData eventData)
    {
		_startPosition = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		_delta -= (_startPosition - eventData.position) / Screen.height * ParallaxCamera.CameraUI.orthographicSize * 2f;
		_startPosition = eventData.position;
	}

	public void Tick(Transform transform, float sens)
	{
		_delta.x *= ParallaxCamera.ParallaxCoefficient;
		transform.Translate(_delta * sens);
		_delta = Vector2.zero;
	}

	public void ReadInput()
	{

	}
}
