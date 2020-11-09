public class HomingModifier : PlayerWeaponModifier
{
    public override string MyEnumName => "HomingModifier";

    public override PlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}
}
