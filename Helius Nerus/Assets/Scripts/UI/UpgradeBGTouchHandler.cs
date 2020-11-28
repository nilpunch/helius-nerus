using UnityEngine;
using UnityEngine.EventSystems;

namespace HNUI
{
	public class UpgradeBGTouchHandler : MonoBehaviour, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			DragAndDropModifier.ResetSelection();
		}
	}
}