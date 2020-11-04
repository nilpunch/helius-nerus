using UnityEngine;

public abstract class Achievment
{

    protected string _achievementName = "";
    protected string _achievementDescription = "";
    protected Sprite _sprite = null;
    protected bool _wasTriggered = false;

    public bool WasTriggered
    {
        get => _wasTriggered;
    }

    public virtual void Init(bool wasTriggered = false)
    {
        _wasTriggered = wasTriggered;
        if (_wasTriggered)
            return;
        Subscribe();
    }

    protected abstract void Subscribe();

    protected abstract void Unsubscribe();

    public void AchievmentHappened()
    {
        _wasTriggered = true;
#if UNITY_EDITOR
        Debug.Log(_achievementName);
#endif
        Unsubscribe();
    }
}
