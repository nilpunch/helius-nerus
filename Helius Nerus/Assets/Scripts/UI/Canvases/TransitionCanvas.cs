namespace HNUI
{
    public class TransitionCanvas : UICanvasMonobehaviour
    {
        public static TransitionCanvas Instance
        {
            get;
            private set;
        } = null;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
    }
}