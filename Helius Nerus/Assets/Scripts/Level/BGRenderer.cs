using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BGRenderer : MonoBehaviour
{
    [SerializeField] private Texture _bgImage = null;
    [SerializeField] private Vector2 _spriteMovementSpeed = Vector2.up;
    [SerializeField] private Renderer _renderer = null;

    private float _time = 0.0f;

    private void Awake()
    {
        _renderer.material.mainTexture = _bgImage;

		float screenHeight = ParallaxCamera.ParallaxSize.y;
		float screenWidth = ParallaxCamera.ParallaxSize.x;

		float bigger = screenWidth > screenHeight ? screenWidth : screenHeight;
        transform.localScale = new Vector3(bigger / 10f, 1.0f, bigger / 10f);
        _renderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    private void Update()
    {
        _time += TimeManager.AnimationDeltaTime;
        _renderer.material.SetTextureOffset("_MainTex", _time * _spriteMovementSpeed);
    }
}