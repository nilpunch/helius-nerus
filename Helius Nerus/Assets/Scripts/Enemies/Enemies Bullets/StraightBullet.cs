using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    void Update()
    {
		transform.Translate(transform.up * 10f * Time.deltaTime);
    }
}
