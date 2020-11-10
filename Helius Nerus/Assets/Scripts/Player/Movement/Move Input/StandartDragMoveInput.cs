using UnityEngine;
using UnityEngine.EventSystems;

public class StandartDragMoveInput : MonoBehaviour, IMoveInput, IDragHandler, IPointerDownHandler
{
	public Vector2 Direction { get; private set; } = Vector2.zero;
	public float Thrust { get; private set; } = 0f;

	private Vector2 _startDrag = Vector2.zero;
	private Vector2 _deltaMove = Vector2.zero;

	public void OnPointerDown(PointerEventData eventData)
	{
		_deltaMove = Vector2.zero;
		_startDrag = ParallaxCamera.CameraUI.ScreenToWorldPoint(eventData.position);
#if UNITY_EDITOR
		Debug.Log("Dragggggg");
#endif
	}

	public void OnDrag(PointerEventData eventData)
	{
		_deltaMove = (Vector2)ParallaxCamera.CameraUI.ScreenToWorldPoint(eventData.position) - _startDrag; 
	}

	public void ReadInput()
	{
		Direction = _deltaMove.normalized;
		Thrust = _deltaMove.magnitude;
		_startDrag -= _deltaMove;
		_deltaMove = Vector2.zero;
	}
}