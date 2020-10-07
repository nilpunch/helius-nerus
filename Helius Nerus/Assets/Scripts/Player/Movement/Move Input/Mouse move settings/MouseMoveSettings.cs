using UnityEngine;

[CreateAssetMenu(menuName = "Controlls/Mouse Move Settings", fileName = "Mouse Move Settings")]
public class MouseMoveSettings : ScriptableObject
{
	[SerializeField] private float _thrustRadius = 0.5f;
	[SerializeField] private float _minThrust = 0.1f;

	public float ThrustRadius => _thrustRadius;
	public float MinThrust => _minThrust;
}