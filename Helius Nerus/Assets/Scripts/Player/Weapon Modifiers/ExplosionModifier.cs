public class ExplosionModifier : PlayerWeaponModifier
{
    // Not implemented yet

    public override string MyEnumName => "ExplosionModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (ExplosionModifier)MemberwiseClone();
	}
}
