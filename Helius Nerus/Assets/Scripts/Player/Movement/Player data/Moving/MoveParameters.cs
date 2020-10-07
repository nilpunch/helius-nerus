using UnityEngine;

[CreateAssetMenu(menuName = "Controlls/Move Parameters", fileName = "Move Parameters")]
public class MoveParameters : ScriptableObject
{
	[SerializeField] private float _moveSpeed = 1f;

	public float MoveSpeed => _moveSpeed;
}

