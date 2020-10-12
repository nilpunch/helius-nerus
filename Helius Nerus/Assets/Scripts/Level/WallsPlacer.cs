using UnityEngine;

public class WallsPlacer : MonoBehaviour
{
    [SerializeField] private Transform _bottomWall = null;
    [SerializeField] private Transform _topWall = null;
    [SerializeField] private Transform _leftWall = null;
    [SerializeField] private Transform _rightWall = null;

    private void Awake()
    {
        Camera camera = Camera.main;
        float screenHeight = camera.orthographicSize;
        float screenWidth = screenHeight * camera.aspect;

        _bottomWall.position = new Vector3(0.0f, -screenHeight - 2, 0.0f);
        _topWall.position = new Vector3(0.0f, screenHeight + 5, 0.0f); // Spawning space
        _bottomWall.localScale = _topWall.localScale = new Vector3(2 * screenWidth + 2, 1.0f, 1.0f);

        _leftWall.transform.position = new Vector3(-screenWidth - 1, 0.0f, 0.0f);
        _rightWall.transform.position = new Vector3(screenWidth + 1, 0.0f, 0.0f);
        _leftWall.localScale = _rightWall.localScale = new Vector3(1.0f, 2 * screenHeight + 10, 0.0f);
    }
}
