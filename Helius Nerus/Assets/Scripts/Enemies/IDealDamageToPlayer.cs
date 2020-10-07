public interface IDealDamageToPlayer
{
    int Damage
    {
        get;
        set;
    }
    int GetMyDamage();
}

public interface IReturnableToPool
{
    void ReturnMeToPool();
}
