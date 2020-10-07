public class BlankModifier : IPlayerWeaponModifier
{
    public IPlayerWeaponModifier Clone()
    {
        return (IPlayerWeaponModifier)this.MemberwiseClone();
    }

    public void OnDestroy()
    {
    }

    public void OnEnable()
    {
    }

    public void OnHit()
    {
    }

    public void Tick()
    {
    }
}