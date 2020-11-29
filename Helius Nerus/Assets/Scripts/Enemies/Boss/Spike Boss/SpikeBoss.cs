using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBoss : Boss
{
	protected override void SetupPhases()
	{
		_phases[0] = new FirstSpikeBossPhase();
		_phases[1] = new SecondSpikeBossPhase();
		_phases[2] = new ThirdSpikeBossPhase();
	}
}
