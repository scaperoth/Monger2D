using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField]
    int enemyKnightLayer = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == enemyKnightLayer)
        {
            PooledObject other = collision.gameObject.GetComponent<PooledObject>();
            other.Finish();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("COLLSION EXIT");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("COLLISION STAY");
    }
}
