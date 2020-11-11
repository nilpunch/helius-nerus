using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text _text = null;

    // Update is called once per frame
    void Update()
    {
        _text.text = "FPS " + (int)(1.0f / Time.deltaTime);   
    }
}
