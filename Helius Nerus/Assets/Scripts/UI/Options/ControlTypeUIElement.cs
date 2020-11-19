using UnityEngine;
using UnityEngine.EventSystems;

namespace HNUI
{
    public class ControlTypeUIElement : MonoBehaviour, IPointerClickHandler
    {
        public string ControlType
        {
            get => _inputType.ToString();
        }

        [SerializeField] private InputType _inputType = InputType.DragMovement;

        [SerializeField] private ControlTypeChanger _controlTypeChanger = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            _controlTypeChanger.ChangeControlType((int)_inputType);
        }
    }
}



