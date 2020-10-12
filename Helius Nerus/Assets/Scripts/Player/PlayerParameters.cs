using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : ScriptableObject
{
	[Tooltip("Максимальное количество здоровья")]
	[SerializeField] private int _maxHealth = 4;
	[Tooltip("Время неуязвимости")]
	[SerializeField] private float _invinsibilityTime = 1.0f;
	[Tooltip("Список пушек персонажа")]
	[SerializeField] private PlayerWeapon[] _weapons = null;

	private int _currentHelath = 0;

	public int CurrentHelath { get => _currentHelath; set => _currentHelath = value; }
	public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
	public float InvinsibilityTime { get => _invinsibilityTime; set => _invinsibilityTime = value; }
	public PlayerWeapon[] Weapons { get => _weapons; set => _weapons = value; }
}
