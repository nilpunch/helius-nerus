using UnityEngine;
using UnityEngine.UI;

namespace HNUI
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Image[] _healthImages = null;

        private void OnEnable()
        {
            RedrawHealth();

            Player.PlayerHealthChanged += Player_PlayerHealthChanged;
        }

        private void Player_PlayerHealthChanged()
        {
            RedrawHealth();
        }

        private void RedrawHealth()
        {
            if (Player.Instance == null)
                return;
            int maxPlayerHealth = Player.PlayerParameters.MaxHealth;
            int curPlayerHealth = Player.PlayerParameters.CurrentHealth;

            if (maxPlayerHealth >= 10)
                maxPlayerHealth = 10;

            for (int i = 0; i < maxPlayerHealth; ++i)
            {
                _healthImages[i].enabled = true;
            }
            for (int i = maxPlayerHealth; i < 10; ++i)
            {
                _healthImages[i].enabled = false;
            }

            for (int i = 0; i < maxPlayerHealth; ++i)
            {
                if (i < curPlayerHealth)
                    _healthImages[i].color = Color.white;
                else
                    _healthImages[i].color = Color.gray;
            }
        }

        private void OnDisable()
        {
            Player.PlayerHealthChanged -= Player_PlayerHealthChanged;
        }
    }
}