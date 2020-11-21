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
        }

        private void OnEnable()
        {
            TransitionScene.NewSceneWasLoaded += TransitionScene_NewSceneWasLoaded;
            Pause.GamePaused += Pause_GamePaused;
        }

        private void Pause_GamePaused()
        {
            _pauseCanvas.enabled = true;
        }

        private void TransitionScene_NewSceneWasLoaded(Scenes obj)
        {
            if (obj == Scenes.INGAME)
            {
                Instance._canvas.enabled = true;
            }
            else
            {
                Instance._canvas.enabled = false;
            }
        }

        private void OnDisable()
        {
            TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
            Pause.GamePaused -= Pause_GamePaused;
        }
    }
}