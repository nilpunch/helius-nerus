using UnityEngine;

namespace HNUI
{
    public class UICanvasMonobehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}