using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform _buttonTransform = null;
    [SerializeField] private RectTransform _baseTransform = null;
    [SerializeField] private float _maxDistance = 5.0f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _baseTransform.anchoredPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetButtonPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _buttonTransform.anchoredPosition = Vector2.zero;
    }

    private void SetButtonPosition(Vector2 position)
    {
        Vector2 direction = position - _baseTransform.anchoredPosition;
        _buttonTransform.anchoredPosition = direction.ClampInBorders(_maxDistance);
    }
}
