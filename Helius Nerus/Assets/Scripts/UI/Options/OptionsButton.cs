using UnityEngine;
using DG.Tweening;

namespace HNUI
{
    public class OptionsButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _menuCanvas = null;
        [SerializeField] private RectTransform _optionsCanvas = null;
        private int _optionsOffset;

        private void Awake()
        {
            _optionsOffset = Screen.width;
        }

        public void MoveToOptions()
        {
            _menuCanvas.DOAnchorPosX(-_optionsOffset, 1.0f);
            _optionsCanvas.DOAnchorPosX(0, 1.0f);
        }

        public void MoveToMenu()
        {
            _menuCanvas.DOAnchorPosX(0, 1.0f);
            _optionsCanvas.DOAnchorPosX(_optionsOffset, 1.0f);
        }
    }
}


