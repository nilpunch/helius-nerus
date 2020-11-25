namespace HNUI
{
    public class AchievementsCanvas : UICanvasMonobehaviour
    {
        public static AchievementsCanvas Instance
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