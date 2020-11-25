using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HNUI
{
	public class UpgradeController : MonoBehaviour
	{
		public static event System.Action ModifierApplyed = delegate { };

		[SerializeField] private DragModifier[] _modifiers = null;
		[SerializeField] private WeaponDragTarget[] _weapons = null;

		private void Awake()
		{
			DragModifier.ModifierDropped += DragModifier_ModifierDropped;
		}

		private void DragModifier_ModifierDropped(DragModifier droppedModifier)
		{
			for (int i = 0; i < _weapons.Length; ++i)
			{	
				if (droppedModifier.RectTransform.rect.Overlaps(_weapons[i].RectTransform.rect))
				{
					ApplyModifier(_weapons[i], droppedModifier);
					return;
				}
			}	
		}

		private void ApplyModifier(WeaponDragTarget weapon, DragModifier droppedModifier)
		{
			weapon.ApplyModifier(droppedModifier.RelatedUpgradePair);

			for (int j = 0; j < _modifiers.Length; ++j)
			{
				if (ReferenceEquals(_modifiers[j], droppedModifier) == false)
				{
					WeaponModifierContainer.Instance.ReturnUnlockedMod(_modifiers[j].RelatedUpgradePair.ModifierID);
				}
			}

			ModifierApplyed.Invoke();
			TransitionScene.Instance.LoadUnloadScene((int)Scenes.INGAME);
		}

		public void ActivateModifierSelection()
		{
			ArtifactUpgradePair[] upgrades = new ArtifactUpgradePair[3];
			for (int i = 0; i < 3; ++i)
			{
				upgrades[i] = ArtifactUpgradePair.CreateRandomPair();
				_modifiers[i].PopulateWithUpgradePair(upgrades[i]);
			}

			for (int i = 0; i < Player.PlayerWeapons.Length; ++i)
			{
				_weapons[i].RelatedWeapon = Player.PlayerWeapons[i];
			}
		}
	}
}