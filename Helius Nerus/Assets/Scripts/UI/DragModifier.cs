using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HNUI
{
	public class DragModifier : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		public static event System.Action<DragModifier> ModifierDropped = delegate { };
		public static event System.Action<DragModifier> SelectionChanged = delegate { };
		private static DragModifier _selectedModifier = null;

		[SerializeField] private RectTransform _transform;
		[SerializeField] private Image _image;
		

		private ArtifactUpgradePair _relatedUpgrade = null;
		private Vector2 _startPosition = Vector2.zero;
		private bool _wasDragged = false;

		public static DragModifier SelectedModifier
		{
			get => _selectedModifier;
			private set
			{
				_selectedModifier = value;
				SelectionChanged.Invoke(_selectedModifier);
			}
		}

		public RectTransform RectTransform
		{
			get => _transform;
		}

		public ArtifactUpgradePair RelatedUpgradePair
		{
			get => _relatedUpgrade;
		}

		public static void ResetSelection()
		{
			SelectedModifier = null;
		}

		private void Awake()
		{
			_startPosition = _transform.anchoredPosition;
		}

		public void PopulateWithUpgradePair(ArtifactUpgradePair upgradePair)
		{
			_relatedUpgrade = upgradePair;
			_image.sprite = upgradePair.ModifierIcon;
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
				ModifierDropped.Invoke(this);
				_transform.anchoredPosition = _startPosition;
				_wasDragged = false;
			}
		}
	}
}