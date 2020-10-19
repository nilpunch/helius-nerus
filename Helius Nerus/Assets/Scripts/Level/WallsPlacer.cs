using UnityEngine;

[System.Serializable]
public class WallsPlacer
{
    [SerializeField] private Transform _bottomWall = null;
    [SerializeField] private Transform _topWall = null;
    [SerializeField] private Transform _leftWall = null;
    [SerializeField] private Transform _rightWall = null;

    public WallsPlacer()
    { }

    public void Init()
    {
        float screenHeight = ParallaxCamera.ParallaxSize.y / 2;
        float screenWidth = ParallaxCamera.ParallaxSize.x / 2;

        _bottomWall.position = new Vector3(0.0f, -screenHeight - 2, 0.0f);
        _topWall.position = new Vector3(0.0f, screenWidth + 5, 0.0f); // Spawning space
        _bottomWall.localScale = _topWall.localScale = new Vector3(2 * screenWidth + 6, 1.0f, 1.0f);

        _leftWall.transform.position = new Vector3(-screenWidth - 3, 0.0f, 0.0f);
        _rightWall.transform.position = new Vector3(screenWidth + 3, 0.0f, 0.0f);
        _leftWall.localScale = _rightWall.localScale = new Vector3(1.0f, 2 * screenHeight + 10, 0.0f);
    }
}
