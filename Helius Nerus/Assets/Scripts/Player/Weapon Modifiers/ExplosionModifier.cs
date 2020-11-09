public class ExplosionModifier : PlayerWeaponModifier
{
    public override string MyEnumName => "ExplosionModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (ExplosionModifier)MemberwiseClone();
	}
}
