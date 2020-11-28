using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.EventSystems;

namespace HNUI
{
	public class WeaponDropTarget : MonoBehaviour, IPointerDownHandler
	{
		public static event System.Action<PlayerWeapon> WeaponSelected = delegate { };

		[SerializeField] private RectTransform _transform = null;

		public PlayerWeapon RelatedWeapon { get; set; } = null;
		public DragAndDropModifier AttachedModifyer { get; private set; } = null;

		public RectTransform RectTransform
		{
			get => _transform;
		}

		public void AttachModifyer(DragAndDropModifier modifier)
		{
			if (ReferenceEquals(AttachedModifyer, null) == false)
			{
				AttachedModifyer.ResetPosition();
			}
			AttachedModifyer = modifier;
			AttachedModifyer.RectTransform.DOKill();
			AttachedModifyer.RectTransform.DOMove(_transform.position, 0.5f).SetEase<Tween>(Ease.OutCubic);
		}

		public void DetachModifier()
		{
			if (ReferenceEquals(AttachedModifyer, null) == false)
			{
				AttachedModifyer.ResetPosition();
			}
			AttachedModifyer = null;
		}

		public void ApplyModifier()
		{
			RelatedWeapon.ApplyPair(AttachedModifyer.RelatedUpgradePair);
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			WeaponSelected.Invoke(RelatedWeapon);
		}
	}
}