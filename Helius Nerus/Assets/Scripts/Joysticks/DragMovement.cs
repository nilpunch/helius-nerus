using UnityEngine;
using UnityEngine.EventSystems;

public class DragMovement : InputCanvas<DragMovement>, IPointerUpHandler, IPointerDownHandler, IDragHandler, IMoveInput
{
	private Vector2 _startPosition = Vector2.zero;

	private Transform _transform;

	public static void InitWithTransform(Transform transform)
	{
		((DragMovement)Instance)._transform = transform;
	}

	protected override string InputType
    {
        get => "DragMovement";
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		_startPosition = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
    {
		_startPosition = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 delta = -(_startPosition - eventData.position) / Screen.height * ParallaxCamera.CameraUI.orthographicSize * 2f;
		delta.x *= ParallaxCamera.ParallaxCoefficient;
		_transform.Translate(delta);
		_startPosition = eventData.position;
	}

	public void Tick(Transform transform)
	{
		 
	}

	public void ReadInput()
	{

	}
}
