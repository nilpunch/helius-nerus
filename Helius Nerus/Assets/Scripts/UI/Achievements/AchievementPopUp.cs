using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace HNUI
{
    public class AchievementPopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform = null;
        [SerializeField] private Image _image = null;
        [SerializeField] private TMPro.TextMeshProUGUI _achievementName = null;

        public bool Finished
        {
            get;
            private set;
        } = true;

        public void ShowAchievement(Achievment achievment, System.Action OnComplete)
        {
            Finished = false;
            _image.sprite = achievment.Sprite;
            _achievementName.text =  LocalizationManager.Instance.GetLocalizedValue(achievment.Name);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectTransform.DOMoveY(50, 1.0f));
            sequence.AppendInterval(5.0f);
            sequence.Append(_rectTransform.DOMoveY(-50, 1.0f));
            sequence.OnComplete(()=>OnComplete());

            Finished = true;
        }

    }
}


