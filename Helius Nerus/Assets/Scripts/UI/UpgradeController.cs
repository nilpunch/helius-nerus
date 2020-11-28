using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HNUI
{
	public class UpgradeController : MonoBehaviour
	{
		public static event System.Action ModifierApplyed = delegate { };

		[SerializeField] private DragAndDropModifier[] _modifiers = null;
		[SerializeField] private WeaponDropTarget[] _weapons = null;

		private void Awake()
		{
			DragAndDropModifier.ModifierDropped += DragModifier_ModifierDropped;
		}

		private void OnDestroy()
		{
			DragAndDropModifier.ModifierDropped -= DragModifier_ModifierDropped;
		}

		private void DragModifier_ModifierDropped(DragAndDropModifier droppedModifier)
		{
			for (int i = 0; i < _weapons.Length; ++i)
			{
				if (ReferenceEquals(_weapons[i].AttachedModifyer, droppedModifier))
				{
					_weapons[i].DetachModifier();
				}
			}

			for (int i = 0; i < _weapons.Length; ++i)
			{
				if (_weapons[i].RectTransform.GetHelathyRect().Contains(droppedModifier.RectTransform.position))
				{ 
					ApplyModifier(_weapons[i], droppedModifier);
					return;
				}
			}
			droppedModifier.ResetPosition();
		}

		private void ApplyModifier(WeaponDropTarget weapon, DragAndDropModifier droppedModifier)
		{
			weapon.AttachModifyer(droppedModifier);

			for (int i = 0; i < _weapons.Length; ++i)
			{
				if (_weapons[i].AttachedModifyer == null)
				{
					return;
				}
			}

			for (int i = 0; i < _modifiers.Length; ++i)
			{
				bool modifierNotUsed = true;

				for (int j = 0; j < _weapons.Length; ++j)
				{
					if (ReferenceEquals(_modifiers[i].RelatedUpgradePair, _weapons[j].AttachedModifyer.RelatedUpgradePair) == true)
					{
						modifierNotUsed = false;
						break;
					}
				}

				if (modifierNotUsed)
				{
					WeaponModifierContainer.Instance.ReturnUnlockedMod(_modifiers[i].RelatedUpgradePair.ModifierID);
				}
			}

			for (int i = 0; i < _weapons.Length; ++i)
			{
				_weapons[i].ApplyModifier();
				_weapons[i].DetachModifier();
			}

			DragAndDropModifier.ResetSelection();
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