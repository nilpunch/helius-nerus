public class TestBoss : Boss
{
    protected override void SetupPhases()
    {
        _phases[0] = new TestBossPhase();
        _phases[1] = new TestBossPhase();
        _phases[2] = new TestBossPhase();
    }
}