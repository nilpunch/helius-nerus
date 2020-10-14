using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BGRenderer : MonoBehaviour
{
    [SerializeField] private Texture _bgImage = null;
    [SerializeField] private Vector2 _spriteMovementSpeed = Vector2.up;
    private Renderer _renderer;

    private float _time = 0.0f;

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
    }

    private void Update()
    {
        if (Pause.Paused)
            return;

        _time += Time.deltaTime;
        _renderer.material.SetTextureOffset("_MainTex", _time * _spriteMovementSpeed);
    }
}