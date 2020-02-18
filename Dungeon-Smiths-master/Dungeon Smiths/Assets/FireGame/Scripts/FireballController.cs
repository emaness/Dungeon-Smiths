using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public GameObject hose;

    void Update()
    {
        if(gameObject.transform.position.x < -3.6f)
        {
            hose.SendMessage("ApplyDamage", 20.0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hose"))
        {
            hose.SendMessage("ApplyDamage", 20.0f);
            Destroy(gameObject);
        }
    }
}