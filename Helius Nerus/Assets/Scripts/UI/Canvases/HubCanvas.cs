namespace HNUI
{
    // С этим надо что-то сделать
    public class HubCanvas : UICanvasMonobehaviour
    {
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

        private void TransitionScene_NewSceneWasLoaded(Scenes obj)
        {
            if (obj == Scenes.HUB)
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