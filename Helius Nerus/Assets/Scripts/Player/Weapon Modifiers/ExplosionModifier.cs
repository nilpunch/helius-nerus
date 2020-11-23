public class ExplosionModifier : PlayerWeaponModifier
{
    // Not implemented yet
    public override ModifierType MyEnumValue => ModifierType.ExplosionModifier;

    public override PlayerWeaponModifier Clone()
	{
		return (ExplosionModifier)MemberwiseClone();
	}
}
