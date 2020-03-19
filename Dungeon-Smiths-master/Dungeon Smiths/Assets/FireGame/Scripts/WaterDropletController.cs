using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropletController : MonoBehaviour
{
    void Update()
    {
        if(gameObject.transform.position.x > 3.6f)
        {
            Destroy(gameObject); // out of bounds
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Fire"))
        {
            collision.gameObject.SendMessage("ApplyDamage", 10.0f);
        }
    }
}
