public class UpgradeHP : UpgradeBase
{
    public override void UpgradeCharacter(Player player)
    {
        player.IncrementHealth();
    }
}