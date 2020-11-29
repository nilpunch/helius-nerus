using UnityEngine;

public class FirstSpikeBossPhase : BossPhase
{
	protected override void PopulateActions()
	{
		_actions = new BossAction[2];
		_actions[0] = new MoveLeftBossAction();
		_actions[1] = new MoveRightBossAction();
		SetPhasesForActions();
	}
}

public class SecondSpikeBossPhase : BossPhase
{
	protected override void PopulateActions()
	{
		_actions = new BossAction[2];
		_actions[0] = new MoveUpAndDownWithShotgun();
		_actions[1] = new MoveUpAndDownWithShotgun();
		SetPhasesForActions();
	}
}

public class ThirdSpikeBossPhase : BossPhase
{
	protected override void PopulateActions()
	{
		_actions = new BossAction[1];
		_actions[0] = new StayStillAndSpray();
		SetPhasesForActions();
	}
}