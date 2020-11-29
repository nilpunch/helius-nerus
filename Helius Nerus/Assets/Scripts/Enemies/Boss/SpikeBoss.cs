using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBoss : Boss
{
	protected override void SetupPhases()
	{
		_phases[0] = new FirstBossPhase();
		_phases[1] = new SecondBossPhase();
		_phases[2] = new ThirdBossPhase();
	}
}
