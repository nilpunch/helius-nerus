namespace HNUI
{
    public class PauseCounterCanvas : UICanvasMonobehaviour
    {
        [UnityEngine.SerializeField]
        private UnityEngine.Canvas _pauseCanvas = null;

        public static PauseCounterCanvas Instance
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
            Pause.GamePaused += Pause_GamePaused;
            Pause.GameUnpaused += Pause_GameUnpaused;
        }

        private void Pause_GameUnpaused()
        {
            _pauseCanvas.enabled = false;
        }

        private void Pause_GamePaused()
        {
            _pauseCanvas.enabled = true;
        }

        private void TransitionScene_NewSceneWasLoaded(Scenes obj)
        {
            if (obj == Scenes.INGAME)
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

        private void OnDisable()
        {
            Pause.GamePaused -= Pause_GamePaused;
            Pause.GameUnpaused -= Pause_GameUnpaused;
        }

        private void OnDestroy()
        {
            TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
        }
    }
}