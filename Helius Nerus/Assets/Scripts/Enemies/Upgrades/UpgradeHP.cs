public class UpgradeHP : UpgradeBase
{
    public override void UpgradeCharacter()
    {
        Player.Instance.IncrementHealth();
        Destroy(gameObject);
    }
}