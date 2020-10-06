using UnityEngine;

[CreateAssetMenu(menuName = "Controlls/Keyboard Move Settings", fileName = "Keyboard Move Settings")]
public class KeyboardMoveSettings : ScriptableObject
{
	[SerializeField] private float _thrust = 1f;

	public float Thrust => _thrust;
}
