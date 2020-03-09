using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BlockCollisionController : MonoBehaviour
{

    public GameObject BlockController;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Hit character");
        if (collider.gameObject.CompareTag("Player"))
        {
            GetComponent <AudioSource>().Play();
            //BlockController.SendMessage("DoLose");
            //Debug.Log("with tag Player");

        }

    }
}
