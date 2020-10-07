public interface IPlayerWeaponModifier
{
    void OnHit();
    void OnDestroy();
    void OnEnable();
    void Tick();
    IPlayerWeaponModifier Clone();
}