using UnityEngine;

namespace HNUI
{
    public class SensitivitySlider : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider = null;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Sensitivity"))
                _slider.value = PlayerPrefs.GetFloat("Sensitivity");
        }


        public void SliderValueChanged(float sens)
        {
            PlayerPrefs.SetFloat("Sensitivity", sens);
        }
    }
}



