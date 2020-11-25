using UnityEngine;

namespace HNUI
{
    public class HubButtonChange : MonoBehaviour
    {
        [SerializeField] private bool _nextShip = true;
        
        public void OnClick()
        {
            PlayersCreator.Instance.ChangePlayer(_nextShip);
        }
    }
}


