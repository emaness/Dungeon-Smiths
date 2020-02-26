using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Skeleton"))
        {
            transform.parent.gameObject.SendMessage("StartWin");

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}