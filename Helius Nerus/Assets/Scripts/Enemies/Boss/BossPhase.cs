using UnityEngine;

public abstract class BossPhase
{
    protected BossAction[] _actions = null;
    protected int _currentAction = 0;

    public BossPhase()
    {
        PopulateActions();
    }

    protected abstract void PopulateActions();
    protected virtual void SetPhasesForActions()
    {
        foreach(BossAction action in _actions)
        {
            action.BossPhase = this;
        }
    }


    public virtual void StartPhase()
    {
        _actions[_currentAction].StartAction();
    }
    public virtual void StopPhase()
    {
        _actions[_currentAction].StopAction();
    }


    public void ChangeAction()
    {
        _actions[_currentAction].StopAction();

        _currentAction = GetAnotherRandomAction();

        _actions[_currentAction].StartAction();
    }

    protected int GetAnotherRandomAction()
    {
        int result = Random.Range(0, _actions.Length - 1);
        if (result >= _currentAction)
            ++result;
        return result;
    }
}

public class TestBossPhase : BossPhase
{
    protected override void PopulateActions()
    {
        _actions = new BossAction[2];
        _actions[0] = new MoveLeftBossAction();
        _actions[1] = new MoveRightBossAction();
        SetPhasesForActions();
    }
}
