namespace HNUI
{
    public class MainMenuCanvas : UICanvasMonobehaviour
    {
        public static MainMenuCanvas Instance
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
            if (obj == Scenes.MENU)
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