using UnityEngine;

public abstract class Achievment
{
    public static event System.Action<Achievment> AchievementHappen = delegate { };

    protected string _achievementName = "";
    protected string _achievementDescription = "";
    protected Sprite _sprite = null;
    protected bool _wasTriggered = false;

    public bool WasTriggered
    {
        get => _wasTriggered;
    }
    public string Name
    {
        get => _achievementName;
    }
    public Sprite Sprite
    {
        get => _sprite;
    }


    public virtual void Init(bool wasTriggered = false)
    {
        _wasTriggered = wasTriggered;
        if (_wasTriggered)
            return;
        Subscribe();
    }

    public void Reset()
    {
        if (_wasTriggered)
        {
            Unsubscribe();
            _wasTriggered = false;
        }
        Init();
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
        AchievementSystem.Instance.SaveAchievments();

        AchievementHappen.Invoke(this);
    }
}
