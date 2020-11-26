using UnityEngine;
using UnityEngine.UI;

namespace HNUI
{
    public class ArtifactsController : MonoBehaviour
    {
        [SerializeField] private Image[] _artifactsImages = null;

        private void OnEnable()
        {
            RedrawArtifacts();

            Player.PlayerArtifactsChanged += Player_PlayerArtifactsChanged;
        }

        private void Player_PlayerArtifactsChanged()
        {
            RedrawArtifacts();
        }


        private void RedrawArtifacts()
        {
            if (Player.Instance == null)
                return;
            int artifactsAmount = Player.PlayerArtifacts.Count;
            if (artifactsAmount >= 5)
                artifactsAmount = 5;
            for (int i = 0; i < artifactsAmount; ++i)
            {
                _artifactsImages[i].enabled = true;
            }
            for (int i = artifactsAmount; i < 5; ++i)
            {
                _artifactsImages[i].enabled = false;
            }
            for (int i = 0; i < artifactsAmount; ++i)
            {
                _artifactsImages[i].sprite = PlayerArtifactContainer.Instance.GetValueByKey(Player.PlayerArtifacts[i].MyEnum).Icon;
            }
        }

        private void OnDisable()
        {
            Player.PlayerArtifactsChanged -= Player_PlayerArtifactsChanged;
        }
    }
}