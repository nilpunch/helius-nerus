using UnityEngine;

public class ScreenBottomTemporary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IReturnableToPool returnableToPool = (collision.gameObject.GetComponent(typeof(IReturnableToPool)) as IReturnableToPool);
        if (returnableToPool != null)
        {
            returnableToPool.ReturnMeToPool();
        }
    }
}
