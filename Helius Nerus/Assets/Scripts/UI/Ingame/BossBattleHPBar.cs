using UnityEngine;
using UnityEngine.UI;

namespace HNUI
{
    public class BossBattleHPBar : MonoBehaviour
    {
        [SerializeField] private Image _bossBattleHPBar = null;
        [SerializeField] private Image _bossBattleHPBack = null;

        private void OnEnable()
        {
            Boss.BossFightBegins += Boss_BossFightBegins;
            Boss.BossDied += Boss_BossDied;
            _bossBattleHPBar.enabled = false;
            _bossBattleHPBack.enabled = false;
        }

        private void Boss_BossDied(int obj)
        {
            _bossBattleHPBar.enabled = false;
            _bossBattleHPBack.enabled = false;
            Boss.BossTakeDamage -= Boss_BossTakeDamage;
        }

        private void Boss_BossFightBegins()
        {
            _bossBattleHPBar.enabled = true;
            _bossBattleHPBack.enabled = true;
            Boss.BossTakeDamage += Boss_BossTakeDamage;
        }

        private void Boss_BossTakeDamage()
        {
            _bossBattleHPBar.fillAmount = Boss.Instance.HPPercentage;
        }

        private void OnDisable()
        {
            Boss.BossFightBegins -= Boss_BossFightBegins;
            Boss.BossDied -= Boss_BossDied;
            Boss.BossTakeDamage -= Boss_BossTakeDamage;
        }
    }
}