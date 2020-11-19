using UnityEngine;

namespace HNUI
{
    public class UICanvasMonobehaviour : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas = null;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}