using UnityEngine;
using UnityEngine.EventSystems;

namespace HNUI
{
    public class ControlTypeUIElement : MonoBehaviour, IPointerClickHandler
    {
        public string ControlType
        {
            get => _controlType;
        }

        [SerializeField] private string _controlType = "DragMovement";
        [SerializeField] private int _myNum = 0;

        [SerializeField] private ControlTypeChanger _controlTypeChanger = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            _controlTypeChanger.ChangeControlType(_myNum);
        }
    }
}



