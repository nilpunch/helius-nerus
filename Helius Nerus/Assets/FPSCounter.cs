using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
    private Text _text = null;

    private void Awake()
    {
        _text = GetComponent<Text>();
        Application.targetFrameRate = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "FPS " + (int)(1.0f / Time.deltaTime);   
    }
}
