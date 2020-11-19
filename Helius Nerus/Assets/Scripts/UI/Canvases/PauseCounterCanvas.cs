namespace HNUI
{
    public class PauseCounterCanvas : UICanvasMonobehaviour
    {
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

        private void OnDestroy()
        {
            TransitionScene.NewSceneWasLoaded -= TransitionScene_NewSceneWasLoaded;
        }
    }
}