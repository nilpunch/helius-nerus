using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;

namespace HNUI
{
	public class DragAndDropModifier : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IEndDragHandler
	{
		public static event System.Action<DragAndDropModifier> ModifierDropped = delegate { };
		public static event System.Action<DragAndDropModifier> SelectionChanged = delegate { };
		private static DragAndDropModifier _selectedModifier = null;

		[SerializeField] private RectTransform _transform = null;
		[SerializeField] private Image _image = null;

		private ArtifactUpgradePair _relatedUpgrade = null;
		private Vector2 _startPosition = Vector2.zero;
		private bool _wasDragged = false;

		public static DragAndDropModifier SelectedModifier
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

		public void ResetPosition()
		{
			_transform.DOKill();
			_transform.DOAnchorPos(_startPosition, 0.5f).SetEase<Tween>(Ease.OutCubic);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_wasDragged = true;
			_transform.DOKill();
			_transform.position = eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			_transform.anchoredPosition += eventData.delta;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			SelectedModifier = this;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_wasDragged)
			{
				ModifierDropped.Invoke(this);
				_wasDragged = false;
			}
		}
	}
}

