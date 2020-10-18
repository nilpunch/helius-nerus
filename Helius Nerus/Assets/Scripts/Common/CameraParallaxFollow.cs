using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallaxFollow : MonoBehaviour
{
	private static CameraParallaxFollow _sceneInstance = null;

	static public float ParallaxCoefficient
	{
		get => _sceneInstance._parallaxCoefficient;
	}
	static public float TranslationCoefficient
	{
		get => _sceneInstance._translationCoefficient;
	}
	static public Transform CameraTransform
	{
		get => _sceneInstance._cameraTransform;
	}
	static public Vector2 CameraSize
	{
		get => _sceneInstance._cameraSize;
	}
	static public Rect CameraRect
	{
		get => _sceneInstance._cameraRect;
	}
	static public Rect GameBoundaryRect
	{
		get => _sceneInstance._parallaxCameraRect;
	}
	static public Vector2 GameSize
	{
		get => _sceneInstance._parallaxCameraSize;
	}

	[SerializeField] private float _parallaxCoefficient = 1.0f;
	private float _translationCoefficient;
	private Transform _cameraTransform;
	private Camera _cameraMain;
	private Vector2 _cameraSize;
	private Vector2 _parallaxCameraSize;
	private Rect _cameraRect;
	private Rect _parallaxCameraRect;


	private void Awake()
	{
		_sceneInstance = this;
		_cameraMain = Camera.main;

		_translationCoefficient = 1f - 1f / _parallaxCoefficient;

		_cameraTransform = _cameraMain.transform;
		_cameraSize.y = _cameraMain.orthographicSize * 2f;
		_cameraSize.x = _cameraSize.y * _cameraMain.aspect;

		_cameraRect.x = _cameraTransform.position.x - _cameraSize.x / 2f;
		_cameraRect.y = _cameraTransform.position.y - _cameraSize.y / 2f;
		_cameraRect.width = _cameraSize.x;
		_cameraRect.height = _cameraSize.y;

		_parallaxCameraSize.x = _cameraSize.x * _parallaxCoefficient;
		_parallaxCameraSize.y = _cameraSize.y;

		_parallaxCameraRect.x = _cameraTransform.position.x - _parallaxCameraSize.x / 2f;
		_parallaxCameraRect.y = _cameraRect.y;
		_parallaxCameraRect.width = _parallaxCameraSize.x;
		_parallaxCameraRect.height = _parallaxCameraSize.y;
	}
}
