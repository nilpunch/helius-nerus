using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Achievment
{
    public bool WasTriggered
    {
        get => _wasTriggered;
    }

    [Header("Строки - коды локализатора!")]
    [SerializeField] private string _achievementName = "";
    [SerializeField] private string _achievementDescription = "";
    [SerializeField] private Sprite _sprite = null;

    [Space]
    [Header("Строки - названия класса и события")]
    [SerializeField] private string _eventClass = "";
    [SerializeField] private string _eventName = "";

    // For reflection
    private Delegate _del = null;
    private EventInfo _objectEventInfo = null;

    private bool _wasTriggered = false;
    private bool _subscribed = false;

    public void Init(bool wasTriggered = false)
    {
        _wasTriggered = wasTriggered;
        if (_wasTriggered)
            return;
        Subscribe();
    }

    private void Subscribe()
    {
        if (_eventClass == "")
        {
#if UNITY_EDITOR
            Debug.LogError("Achievment " + LocalizationManager.Instance.GetLocalizedValue(_achievementName) + " has no class name!");
#endif
            return;
        }
        if (_eventName == "")
        {
#if UNITY_EDITOR
            Debug.LogError("Achievment " + LocalizationManager.Instance.GetLocalizedValue(_achievementName) + " has no event name!");
#endif
            return;
        }

        Type objectType = Type.GetType(_eventClass);
        if (objectType == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Achievment " + LocalizationManager.Instance.GetLocalizedValue(_achievementName) + " has wrong class name!");
#endif
            return;
        }

        _objectEventInfo = objectType.GetEvent(_eventName);
        if (_objectEventInfo == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Achievment " + LocalizationManager.Instance.GetLocalizedValue(_achievementName) + " has wrong event name!");
#endif
            return;
        }

        Type objectEventHandlerType = _objectEventInfo.EventHandlerType;
        MethodInfo mi = this.GetType().GetMethod("AchievmentHappened");
        _del = Delegate.CreateDelegate(objectEventHandlerType, this, mi);
        _objectEventInfo.AddEventHandler(this, _del);

        _subscribed = true;
    }

    private void Unsubscribe()
    {
        if (_subscribed)
        {
            // unsub
            _objectEventInfo.RemoveEventHandler(this, _del);
            _subscribed = false;
        }
    }

    public void AchievmentHappened()
    {
        _wasTriggered = true;
#if UNITY_EDITOR
		Debug.Log(_achievementName);
#endif
        Unsubscribe();
	}
}
