namespace HNUI
{
    // С этим надо что-то сделать
    public class HubCanvas : UICanvasMonobehaviour
    {
        [UnityEngine.SerializeField] private TMPro.TextMeshProUGUI _shipDescrText = null;
        [UnityEngine.SerializeField] private UnityEngine.UI.Image _shipImage = null;

        public static HubCanvas Instance
        {
            get;
            private set;
        } = null;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;

            TransitionScene.NewSceneWasLoaded += TransitionScene_NewSceneWasLoaded;
        }

        private void OnEnable()
        {
            PlayersCreator.PlayerShipChanged += PlayersCreator_PlayerShipChanged;
            if (Player.Instance != null)
                PlayersCreator_PlayerShipChanged();
        }

        private void OnDisable()
        {
            PlayersCreator.PlayerShipChanged -= PlayersCreator_PlayerShipChanged;
        }

        private void PlayersCreator_PlayerShipChanged()
        {
            _shipDescrText.text = LocalizationManager.Instance.GetLocalizedValue(Player.ShipDescription);
            _shipImage.sprite = Player.SpriteRenderer.sprite;  
            _shipImage.color = Player.SpriteRenderer.color;  
        }

        private void TransitionScene_NewSceneWasLoaded(Scenes obj)
        {
            if (obj == Scenes.HUB)
            {
                //Instance._canvas.enabled = true;
                gameObject.SetActive(true);
            }
            else
            {
                //Instance._canvas.enabled = false;
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
        }
    }
}