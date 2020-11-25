using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDragTarget : MonoBehaviour
{
	[SerializeField] private RectTransform _transform = null;

	public PlayerWeapon RelatedWeapon { get; set; } = null;

	public RectTransform RectTransform
	{
		get => _transform;
	}

	public void ApplyModifier(ArtifactUpgradePair upgradePair)
	{
		RelatedWeapon.ApplyPair(upgradePair);
	}
}
