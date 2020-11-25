using UnityEngine;
using UnityEngine.EventSystems;

namespace HNUI
{
    public class DragModifier : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public static DragModifier SelectedModifier { get; private set; } = null;

        [SerializeField] private RectTransform _transform = null;

        private Vector2 _startPosition = Vector2.zero;
        private bool _wasDragged = false;

        private void Awake()
        {
            _startPosition = _transform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _wasDragged = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _transform.anchoredPosition += eventData.delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectedModifier = this;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_wasDragged)
            {
                _transform.anchoredPosition = _startPosition;
                _wasDragged = false;
            }
        }
    }
}


