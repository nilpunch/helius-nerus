namespace HNUI
{
    public class InGameCanvas : UICanvasMonobehaviour
    {
        public static InGameCanvas Instance
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
            if (obj == Scenes.INGAME || obj == Scenes.TUTORIAL)
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