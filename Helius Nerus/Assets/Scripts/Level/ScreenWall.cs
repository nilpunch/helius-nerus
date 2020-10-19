using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScreenWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IReturnableToPool returnableToPool = (collision.gameObject.GetComponent(typeof(IReturnableToPool)) as IReturnableToPool);
        if (returnableToPool != null)
        {
            returnableToPool.ReturnMeToPool();
        }
    }
}
