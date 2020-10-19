using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
	private static ParallaxCamera _sceneInstance = null;

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
	static public Rect CameraBoundary
	{
		get => _sceneInstance._cameraBoundary;
	}
	static public Rect ParallaxBoundary
	{
		get => _sceneInstance._parallaxBoundary;
	}
	static public Vector2 ParallaxSize
	{
		get => _sceneInstance._parallaxSize;
	}

	[SerializeField] private float _parallaxCoefficient = 1.0f;
	private float _translationCoefficient;
	private Transform _cameraTransform;
	private Vector2 _cameraSize;
	private Vector2 _parallaxSize;
	private Rect _cameraBoundary;
	private Rect _parallaxBoundary;


	private void Awake()
	{
		_sceneInstance = this;
        
		Camera _cameraMain = Camera.main;

		_translationCoefficient = 1f - 1f / _parallaxCoefficient;

		_cameraTransform = _cameraMain.transform;
		_cameraSize.y = _cameraMain.orthographicSize * 2f;
		_cameraSize.x = _cameraSize.y * _cameraMain.aspect;

		_cameraBoundary.x = _cameraTransform.position.x - _cameraSize.x / 2f;
		_cameraBoundary.y = _cameraTransform.position.y - _cameraSize.y / 2f;
		_cameraBoundary.width = _cameraSize.x;
		_cameraBoundary.height = _cameraSize.y;

		_parallaxSize.x = _cameraSize.x * _parallaxCoefficient;
		_parallaxSize.y = _cameraSize.y;

		_parallaxBoundary.x = _cameraTransform.position.x - _parallaxSize.x / 2f;
		_parallaxBoundary.y = _cameraBoundary.y;
		_parallaxBoundary.width = _parallaxSize.x;
		_parallaxBoundary.height = _parallaxSize.y;
	}
}
