using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Change level
        FindObjectOfType<LevelsChanger>().ChangeLevel();
    }
}
