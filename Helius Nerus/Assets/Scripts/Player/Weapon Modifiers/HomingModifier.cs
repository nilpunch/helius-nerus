public class HomingModifier : PlayerWeaponModifier
{
    public override PlayerWeaponModifier Clone()
	{
		return (HomingModifier)MemberwiseClone();
	}
}
