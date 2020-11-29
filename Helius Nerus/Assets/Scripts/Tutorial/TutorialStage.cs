public class TutorialStage
{
    protected TutorialController _controller;
    protected HNUI.HintCanvas _hintCanvas;

    public TutorialStage(TutorialController controller)
    {
        _controller = controller;
        _hintCanvas = HNUI.HintCanvas.Instance;
    }

    public virtual void StartStage()
    {
        _hintCanvas = HNUI.HintCanvas.Instance;
    }

    public virtual void EndStage()
    {

    }
}
