using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
	private static ParallaxCamera _sceneInstance = null;

	[SerializeField] private float _parallaxCoefficient = 1.0f;
    [SerializeField] private Camera _camera = null;

	private float _translationCoefficient;
	private Transform _cameraTransform;
	private Vector2 _cameraSize;
	private Vector2 _parallaxSize;
	private Rect _cameraBoundary;
	private Rect _parallaxBoundary;
	private Camera _cameraUI;

	public static float ParallaxCoefficient
	{
		get => _sceneInstance._parallaxCoefficient;
	}
	public static float TranslationCoefficient
	{
		get => _sceneInstance._translationCoefficient;
	}
	public static Transform CameraTransform
	{
		get => _sceneInstance._cameraTransform;
	}
	public static Vector2 CameraSize
	{
		get => _sceneInstance._cameraSize;
	}
	public static Rect CameraBoundary
	{
		get => _sceneInstance._cameraBoundary;
	}
	public static Rect ParallaxBoundary
	{
		get => _sceneInstance._parallaxBoundary;
	}
	public static Vector2 ParallaxSize
	{
		get => _sceneInstance._parallaxSize;
	}
	public static Camera CameraMain
	{
		get => _sceneInstance._camera;
	}
	public static Camera CameraUI
	{
		get => _sceneInstance._cameraUI;
	}


	private void Awake()
	{
		_sceneInstance = this;

		_cameraUI = GameObject.Find("UI Camera").GetComponent<Camera>();

		_translationCoefficient = 1f - 1f / _parallaxCoefficient;

		_cameraTransform = _camera.transform;
		_cameraSize.y = _camera.orthographicSize * 2f;
		_cameraSize.x = _cameraSize.y * _camera.aspect;

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
