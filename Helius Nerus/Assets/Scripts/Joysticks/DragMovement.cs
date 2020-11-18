using UnityEngine;
using UnityEngine.EventSystems;

public class DragMovement : InputCanvas<DragMovement>, IDragHandler, IPointerDownHandler, IPointerUpHandler, IMoveInput
{
    private Vector2 _clickPosition;

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

    protected override string InputType
    {
        get => "DragMovement";
    }

    public void OnDrag(PointerEventData eventData)
    {
        _direction = eventData.position - _clickPosition;
        _clickPosition = eventData.position;
    }

    public void ReadInput()
    {
        //_direction = ParallaxCamera.CameraUI.ScreenToWorldPoint(new Vector3(Screen.width / 2 + _direction.x,
        //    Screen.height / 2 + _direction.y, 0));

        Direction = _direction.normalized;
        // mag  -lenght in pixels
        // height - height, so we got like percentage of screen after division
        // then multiply by camera size
        Thrust = _direction.magnitude / Screen.height * ParallaxCamera.CameraUI.orthographicSize * 2;
        //Thrust = _direction.magnitude;


        Thrust *= TransformMover.DESCTOP_MAX_SPEED;

        _direction = Vector2.zero;
        //Thrust *= TransformMover.DESCTOP_MAX_SPEED;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _clickPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _direction = Vector2.zero;
    }
}
