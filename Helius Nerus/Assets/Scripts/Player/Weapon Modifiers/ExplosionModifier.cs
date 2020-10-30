using System.Collections;

public class ExplosionModifier : PlayerWeaponModifier
{
    public override PlayerWeaponModifier Clone()
	{
		return (ExplosionModifier)MemberwiseClone();
	}
}
