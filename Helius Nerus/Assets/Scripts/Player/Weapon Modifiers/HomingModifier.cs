public class HomingModifier : PlayerWeaponModifier
{
    // Not implemented yet

    public override string MyEnumName => "HomingModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}
}
