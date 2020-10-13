using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BGRenderer : MonoBehaviour
{
    [SerializeField] private Texture _bgImage = null;
    [SerializeField] private Vector2 _spriteMovementSpeed = Vector2.up;
    private Renderer _renderer;

    private float _time = 0.0f;
    private bool _paused = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.mainTexture = _bgImage;

        //size
        Camera camera = Camera.main;
        float screenHeight = camera.orthographicSize;
        float screenWidth = screenHeight * camera.aspect;

        float bigger = screenWidth > screenHeight ? screenWidth : screenHeight;
        transform.localScale = new Vector3(bigger / 5, 1.0f, bigger / 5);
        _renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;

        Pause.GamePaused += Pause_GamePaused;
        Pause.GameUnpaused += Pause_GameUnpaused;
    }

    private void OnDestroy()
    {
        Pause.GamePaused -= Pause_GamePaused;
        Pause.GameUnpaused -= Pause_GameUnpaused;
    }

    private void Pause_GameUnpaused()
    {
        _paused = false;
    }

    private void Pause_GamePaused()
    {
        _paused = true;
    }

    private void Update()
    {
        if (_paused)
            return;
        _time += Time.deltaTime;
        _renderer.material.SetTextureOffset("_MainTex", _time * _spriteMovementSpeed);
    }

}
